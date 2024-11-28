using DAL.Entities;

namespace DAL.Storage;

public interface IProductRepository
{
  public Task<List<Product>> GetAllProducts();
  public Task<Product> GetProduct(int id);
  public Task AddProduct(Product p);
}
