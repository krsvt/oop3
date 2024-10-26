using shops.Entities;

namespace shops.Storage;

public interface IShopStorage
{

  public Shop GetAllShops();
  public Shop GetShop(int id);
  public Shop AddShop(Product p);
}
