using Shops.Entities;
using Shops.Storage;

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

  public async Task AddShopProductsAsync(int shopId, List<ShopProducts> products)
  {
    await Storage.ShopProductsStorage.AddShopProducts(shopId, products);
  }

  public async Task BuyProducts(int shopId, List<ShopProducts> products)
  {
    await Storage.ShopProductsStorage.BuyProducts(shopId, products);
  }

  public async Task<int> LowerProductPrice(int productId)
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
