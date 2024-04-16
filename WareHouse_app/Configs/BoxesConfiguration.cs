using Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class BoxesConfiguration : IEntityTypeConfiguration<Box>
{
    public void Configure(EntityTypeBuilder<Box> builder)
    {
        builder.HasKey(b => b.Id);
        builder.HasOne(b => b.Pallet).WithMany(p => p.Boxes).HasForeignKey(b => b.PalletId).IsRequired();
    }
}