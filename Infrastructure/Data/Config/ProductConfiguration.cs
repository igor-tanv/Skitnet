using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
  public class ProductConfiguration : IEntityTypeConfiguration<Product>
  {
    public void Configure(EntityTypeBuilder<Product> builder)
    {
      builder.Property(b => b.Id).IsRequired();
      builder.Property(b => b.Name).IsRequired().HasMaxLength(100);
      builder.Property(b => b.Description).IsRequired();
      builder.Property(b => b.Price).HasColumnType("decimal(18,2)");
      builder.Property( b => b.PictureUrl).IsRequired();
      builder.HasOne( t => t.ProductType).WithMany().HasForeignKey(p => p.ProductTypeId);
      builder.HasOne( t => t.ProductBrand).WithMany().HasForeignKey(p => p.ProductBrandId);
    }
  }
}