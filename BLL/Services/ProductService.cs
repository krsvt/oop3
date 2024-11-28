using DAL.Entities;
using DAL.Storage;
using BLL.DTO;

namespace BLL.Services;

public class ProductService
{

  private IStorage Storage;
  public ProductService(IStorage storage)
  {
    Storage = storage;
  }

  public async Task<List<Product>> GetAllProductsAsync()
  {
    return await Storage.ProductRepository.GetAllAsync();
  }

  public async Task<Product> GetProductAsync(int id)
  {
    return await Storage.ProductRepository.GetAsync(id);
  }

  public async Task AddProductAsync(Product product)
  {
    await Storage.ProductRepository.AddAsync(product);
  }

  public async Task<LowerProductPriceResponseDTO> LowerProductPrice(int productId)
  {
    var products = await Storage.ShopProductsRepository
      .GetAsync(sp => sp.ProductId == productId);

    var lowestPriceProduct = products
      .Where(p => p.ProductId == productId)
      .OrderBy(p => p.Price)
      .FirstOrDefault();

    if (lowestPriceProduct == null)
    {
      return new LowerProductPriceResponseDTO
      {
        ShopId = 0,
        Price = 0
      };
    }

    return new LowerProductPriceResponseDTO
    {
      ShopId = lowestPriceProduct.ShopId,
      Price = lowestPriceProduct.Price
    };
  }

    private bool ContainsAllProductsAndAmountIsFine(List<ShopProducts> shopProducts,
            List<BuyRequestDTO> requestedProducts)
    {
        var availableProducts = shopProducts.ToDictionary(sp => sp.ProductId, sp => sp.Amount);

        foreach (var product in requestedProducts)
        {
            if (!availableProducts.TryGetValue(product.Id, out var availableAmount))
            {
                return false;
            }

            if (availableAmount < product.Amount)
            {
                return false;
            }
        }

        return true;
    }

  public async Task<LowerProductPriceResponseDTO> LowerShopProductsPrice(List<BuyRequestDTO> shopProducts)
  {
    var productIds = shopProducts.Select(sp => sp.Id).ToList();
    var products = await Storage.ShopProductsRepository.GetAsync(sp => productIds.Contains(sp.ProductId));

    var availableShops = products
        .GroupBy(p => p.ShopId)
        .Where(g => ContainsAllProductsAndAmountIsFine(g.ToList(), shopProducts))
        .Select(g => new
        {
          ShopId = g.Key,
          Price = g.Min(sp => sp.Price)
        })
        .OrderBy(g => g.Price)
        .FirstOrDefault();

    if (availableShops == null)
      throw new Exception("No suitable shop found");

    return new LowerProductPriceResponseDTO
    {
      ShopId = availableShops.ShopId,
      Price = availableShops.Price
    };
  }

}
