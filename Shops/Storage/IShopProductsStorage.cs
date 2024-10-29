using Shops.Entities;

namespace Shops.Storage;

public interface IShopProductsStorage
{
  public Task SaveProductsAsync(List<ShopProducts> products);
  public Task<List<ShopProducts>> LoadProductsAsync(int shopId);
  public Task<List<ShopProducts>> LoadProductsByProductIdsAsync(List<int> productIds);
}
