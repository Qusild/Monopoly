using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Models;

public class PalletsRepo : IRepository<Pallet>
{
    private readonly WareHouseDbContext _dbContext;

    public PalletsRepo(WareHouseDbContext dbContext) => _dbContext = dbContext;

    public async Task<List<Pallet>> GetAll()
    {
        return await _dbContext.Pallets
                        .Include(p => p.Boxes)
                        .OrderBy(p => p.ExperationDate)
                        .ThenBy(p => p.Weight + p.Boxes.Sum(b=> b.Weight))
                        .ToListAsync();
    }
    public async Task<List<Pallet>> GetPalletsWithTheLongestLifeTimeBoxes()
    {
        return await _dbContext.Pallets
                        .AsNoTracking()
                        .Include(p => p.Boxes.OrderBy(b => b.ExperationDate)
                        .ThenBy(b => b.Height*b.Width*b.Length))
                        .OrderByDescending(p => p.Boxes.Max(b=>b.ExperationDate))
                        .Take(3)
                        .OrderBy(p => p.Height*p.Width*p.Length + p.Boxes.Sum(b => b.Height*b.Width*b.Length))
                        .ToListAsync();
    }
    public async Task AddAsync(Pallet pallet)
    {
        if (_dbContext.Pallets.Where(p => p.Id == pallet.Id).Count()>0)
            throw new ArgumentException("Эта паллета уже есть в базе данных");
        await _dbContext.Pallets.AddAsync(pallet);
        await _dbContext.SaveChangesAsync();
    }
    public async Task DeleteAsync(Guid id)
    {
        var pallet = _dbContext.Pallets.FirstOrDefault(p => p.Id == id) ?? 
            throw new ArgumentException(message: "Удаление несуществующего паллета");
        foreach (Box box in pallet.Boxes)
            await _dbContext.Boxes.Where(b => b.Id == box.Id).ExecuteDeleteAsync();
        await _dbContext.Pallets.Where(p => p.Id == id).ExecuteDeleteAsync();
    }
}