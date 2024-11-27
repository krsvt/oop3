using DAL.Entities;

namespace DAL.Storage;

public interface IProductStorage
{

  public Task<List<Product>> GetAllProducts();
  public Task<Product> GetProduct(int id);
  public Task AddProduct(Product p);
}
