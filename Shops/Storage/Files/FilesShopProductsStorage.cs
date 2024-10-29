using Shops.DTO;
using Shops.Entities;

namespace Shops.Storage.Files;

public class FilesShopProductsStorage : IShopProductsStorage
{

  Task<List<ShopProducts>> IShopProductsStorage.AddShopProducts(int shopId, List<ShopProducts> products)
  {
    throw new NotImplementedException();
  }

  Task<decimal> IShopProductsStorage.BuyProducts(int shopId, List<BuyRequestDTO> ShopProducts)
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

  Task<LowerProductPriceResponseDTO> IShopProductsStorage.LowerShopProductsPrice(List<BuyRequestDTO> products)
  {
    throw new NotImplementedException();
  }

  Task<PossibleProductsResponseDTO> IShopProductsStorage.PossibleProducts(int shopId, PossibleProductsRequestDTO possibleProductsRequest)
  {
    throw new NotImplementedException();
  }
}
