using Shops.Entities;

namespace Shops.Storage;

public interface IShopProductsStorage
{
  public Task AddShopProducts(int shopId);
  public Task<List<ShopProducts>> BuyProducts(int shopId, List<ShopProducts> ShopProducts);
  public Task<List<ShopProducts>> PossibleProducts(int shopId, decimal sum);

  public Task<int> LowerProductPrice(int productId);
  public Task<int> LowerShopProductsPrice(int shopId, List<ShopProducts> ShopProducts);
}
