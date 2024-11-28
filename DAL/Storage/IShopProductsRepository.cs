using DAL.Entities;

namespace DAL.Storage;

public interface IShopProductsRepository
{
  public Task SaveProductsAsync(List<ShopProducts> products);
  public Task<List<ShopProducts>> LoadProductsAsync(int shopId);
  public Task<List<ShopProducts>> LoadProductsByProductIdsAsync(List<int> productIds);
}
