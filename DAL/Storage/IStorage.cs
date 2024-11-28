using DAL.Entities;
namespace DAL.Storage;

public interface IStorage
{
  public IRepository<Product> ProductRepository { get; set; }
  public IRepository<Shop> ShopRepository { get; set; }
  public IRepository<ShopProducts> ShopProductsRepository { get; set; }
}
