using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Models;

public class BoxesRepo : IRepository<Box>
{
    private readonly WareHouseDbContext _dbContext;

    public BoxesRepo(WareHouseDbContext dbContext) => _dbContext = dbContext;

    public async Task AddAsync(Box box)
    {
        var pallet = _dbContext.Pallets.FirstOrDefault(p => p.Id == box.PalletId) ?? 
            throw new ArgumentException("Неверно заданы параметры коробки. Проверьте указанный Id паллеты.");
        if (box.Width>pallet.Width||box.Length>pallet.Length)
            throw new ArgumentException("Неверно заданы параметры коробки. Эта коробка не поместится на выбранную паллету.");
        if (pallet.Boxes.Contains(box))
            throw new ArgumentException("Коробка с этим Id уже числится на паллете.");
        await _dbContext.AddAsync(box);
        pallet.ExperationDate = pallet.ExperationDate>box.ExperationDate||pallet.ExperationDate==null ? 
            box.ExperationDate : pallet.ExperationDate;
        await _dbContext.SaveChangesAsync();
    }
    public async Task DeleteAsync(Guid id)
    {
        var box = _dbContext.Boxes.FirstOrDefault(b => b.Id == id) ?? 
            throw new ArgumentException(message: "Удаление не существующей коробки");
        var pallet = _dbContext.Pallets.First(p => p.Id == box.PalletId);
        pallet.ExperationDate = null;
        var neighbourBoxes = _dbContext.Pallets.First(p => p.Id == box.PalletId).Boxes;
        neighbourBoxes.Remove(box);
        foreach (Box neighbour in neighbourBoxes)
        {
            if (pallet.ExperationDate == null || neighbour.ExperationDate < pallet.ExperationDate)
                pallet.ExperationDate = neighbour.ExperationDate;
        }
        await _dbContext.SaveChangesAsync();
        await _dbContext.Boxes.Where(b => b.Id == id).ExecuteDeleteAsync();
    }

}