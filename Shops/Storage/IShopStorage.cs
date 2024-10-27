using Shops.Entities;

namespace Shops.Storage;

public interface IShopStorage
{

  public Shop GetAllShops();
  public Shop GetShop(int id);
  public Shop AddShop(Product p);
}
