using Shops.Entities;

namespace Shops.Storage;

public interface IShopStorage
{
  public Task<List<Shop>> GetAllShops();
  public Task<Shop> GetShop(int id);
  public Task AddShop(Shop s);
}
