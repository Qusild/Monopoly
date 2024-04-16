using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Models;
public class WareHouseDbContext: DbContext
{
    public WareHouseDbContext(DbContextOptions<WareHouseDbContext> options): base(options)
    {}
    public WareHouseDbContext()
    {}
    public DbSet<Box> Boxes { get; set; }
    public DbSet<Pallet> Pallets { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        StreamReader settingsReader = new StreamReader("./Settings/ConnectionString.txt");;
        optionsBuilder.UseNpgsql(settingsReader.ReadLine());
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BoxesConfiguration());
        modelBuilder.ApplyConfiguration(new PalletsConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}