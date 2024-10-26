using shops.Entities;

namespace shops.Storage;

public interface IProductStorage
{

  public Product GetAllProducts();
  public Product GetProduct(int id);
  public Product AddProduct(Product p);
}
