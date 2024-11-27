using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Storage.Database;

public class MyDbContext : DbContext
{
  public MyDbContext(DbContextOptions<MyDbContext> options)
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
