using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Storage.Database;

public class ShopProductsConfiguration : IEntityTypeConfiguration<ShopProducts>
{
    public void Configure(EntityTypeBuilder<ShopProducts> builder)
    {
        builder.HasKey(sp => new { sp.ShopId, sp.ProductId });
    }
}
