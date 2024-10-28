using Shops.Entities;
using Shops.Storage;
using Shops.DTO;

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

  public async Task<Product> GetProductAsync(int id)
  {
    return await Storage.ProductStorage.GetProduct(id);
  }


  public async Task AddProductAsync(Product product)
  {
    await Storage.ProductStorage.AddProduct(product);
  }

  public async Task<LowerProductPriceResponseDTO> LowerProductPrice(int productId)
  {
    return await Storage.ShopProductsStorage.LowerProductPrice(productId);
  }

}
