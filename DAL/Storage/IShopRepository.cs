using DAL.Entities;

namespace DAL.Storage;

public interface IShopRepository
{
  public Task<List<Shop>> GetAllShops();
  public Task<Shop> GetShop(int id);
  public Task AddShop(Shop s);
}
