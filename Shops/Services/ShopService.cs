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

}
