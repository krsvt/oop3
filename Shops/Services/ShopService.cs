using Shops.Entities;
using Shops.Storage;
using Shops.DTO;

namespace Shops.Services;

public class ShopService
{

  private IStorage Storage;
  public ShopService(IStorage storage)
  {
    Storage = storage;
  }

  public async Task<List<Shop>> GetAllShopsAsync()
  {
    return await Storage.ShopStorage.GetAllShops();
  }

  public async Task<Shop> GetShopAsync(int id)
  {
    return await Storage.ShopStorage.GetShop(id);
  }


  public async Task AddShopAsync(Shop product)
  {
    await Storage.ShopStorage.AddShop(product);
  }

  public async Task<List<ShopProducts>>
    AddShopProductsAsync(int shopId, List<AddProductsRequestDTO> products)
  {
    var productsEntity = products.Select(p => new ShopProducts
        { ShopId = shopId, ProductId = p.ProductId, Amount = p.Amount, Price = p.Price}).ToList();
    return await Storage.ShopProductsStorage.AddShopProducts(shopId, productsEntity);
  }

  public async Task<decimal> BuyProducts(int shopId, List<BuyRequestDTO> products)
  {
    return await Storage.ShopProductsStorage.BuyProducts(shopId, products);
  }

  public async Task<LowerProductPriceResponseDTO> LowerProductPrice(int productId)
  {
    return await Storage.ShopProductsStorage.LowerProductPrice(productId);
  }

  public async Task<int> LowerShopProductsPrice(List<ShopProducts> products)
  {
    return await Storage.ShopProductsStorage.LowerShopProductsPrice(products);
  }

  // later
  public async Task PossibleProducts(int shopId, decimal sum)
  {
    await Storage.ShopProductsStorage.PossibleProducts(shopId, sum);
  }
}
