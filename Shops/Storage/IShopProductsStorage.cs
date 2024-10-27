using Shops.Entities;

namespace Shops.Storage;

public interface IShopProductsStorage
{
  public Task AddShopProducts(int shopId, List<ShopProducts> products);
  public Task<List<ShopProducts>> BuyProducts(int shopId, List<ShopProducts> ShopProducts);

  public Task<int> LowerProductPrice(int productId);
  public Task<int> LowerShopProductsPrice(List<ShopProducts> products);

  public Task<List<ShopProducts>> PossibleProducts(int shopId, decimal sum);
}
