namespace DAL.Storage;

public interface IStorage
{
  public IProductRepository ProductRepository { get; set; }
  public IShopRepository ShopRepository { get; set; }
  public IShopProductsRepository ShopProductsRepository { get; set; }
}
