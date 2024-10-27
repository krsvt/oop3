using Shops.Entities;
using Shops.Storage;

namespace Shops.Services;

public class ProductService
{

  private IStorage Storage;
  public ProductService(IStorage storage)
  {
    Storage = storage;
  }

  public async Task<List<Product>> GetAllProductsAsync()
  {
    return await Storage.ProductStorage.GetAllProducts();
  }

}
