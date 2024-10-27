using Shops.Entities;

namespace Shops.Storage.Files;

public class FilesShopProductsStorage : IShopProductsStorage
{
  Task IShopProductsStorage.AddShopProducts(int shopId, List<ShopProducts> ShopProducts)
  {
    throw new NotImplementedException();
  }

  Task<List<ShopProducts>> IShopProductsStorage.BuyProducts(int shopId, List<ShopProducts> ShopProducts)
  {
    throw new NotImplementedException();
  }

  Task<int> IShopProductsStorage.LowerProductPrice(int productId)
  {
    throw new NotImplementedException();
  }

  Task<int> IShopProductsStorage.LowerShopProductsPrice(List<ShopProducts> products)
  {
    throw new NotImplementedException();
  }

  Task<List<ShopProducts>> IShopProductsStorage.PossibleProducts(int shopId, decimal sum)
  {
    throw new NotImplementedException();
  }
}
