using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Storage.Database;

public class ShopsDbContext : DbContext
{
  public ShopsDbContext(DbContextOptions<ShopsDbContext> options)
      : base(options)
  {
  }

  public DbSet<Product> Products { get; set; } = null!;
  public DbSet<Shop> Shops { get; set; } = null!;
  public DbSet<ShopProducts> ShopProducts { get; set; } = null!;

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<ShopProducts>().HasKey(table => new {table.ShopId, table.ProductId});
  }
}
