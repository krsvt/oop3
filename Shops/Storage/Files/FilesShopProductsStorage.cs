using Shops.DTO;
using Shops.Entities;

namespace Shops.Storage.Files;

public class FilesShopProductsStorage : IShopProductsStorage
{

  Task<List<ShopProducts>> IShopProductsStorage.AddShopProducts(int shopId, List<ShopProducts> products)
  {
    throw new NotImplementedException();
  }

  Task<List<ShopProducts>> IShopProductsStorage.BuyProducts(int shopId, List<ShopProducts> ShopProducts)
  {
    throw new NotImplementedException();
  }

  Task<List<ShopProducts>> IShopProductsStorage.GetProducts(int shopId)
  {
    throw new NotImplementedException();
  }


  Task<LowerProductPriceResponseDTO> IShopProductsStorage.LowerProductPrice(int productId)
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
