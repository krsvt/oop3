namespace Shops.Storage;

public interface IStorage
{

  public IProductStorage ProductStorage { get; set; }
  public IShopStorage ShopStorage { get; set; }
  public IShopProductsStorage ShopProductsStorage { get; set; }

}
