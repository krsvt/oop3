using Shops.Entities;

namespace Shops.Storage;

public interface IProductStorage
{

  public Task<List<Product>> GetAllProducts();
  public Task<Product> GetProduct(int id);
  public Task<Product> AddProduct(Product p);
}
