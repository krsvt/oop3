using DAL.Entities;
using DAL.Storage;
using BLL.DTO;

namespace BLL.Services;

public class ShopService
{

  private IStorage Storage;
  public ShopService(IStorage storage)
  {
    Storage = storage;
  }

  public async Task<List<Shop>> GetAllShopsAsync()
  {
    return await Storage.ShopRepository.GetAllShops();
  }

  public async Task<Shop> GetShopAsync(int id)
  {
    return await Storage.ShopRepository.GetShop(id);
  }


  public async Task AddShopAsync(Shop product)
  {
    await Storage.ShopRepository.AddShop(product);
  }

  public async Task<List<ShopProducts>>
    AddShopProductsAsync(int shopId, List<AddProductsRequestDTO> products)
  {
    var productsEntity = products.Select(p => new ShopProducts
    {
      ShopId = shopId,
      ProductId = p.ProductId,
      Amount = p.Amount,
      Price = p.Price
    }).ToList();

    var existingProducts = await Storage.ShopProductsRepository.LoadProductsAsync(shopId);

    var newProducts = new List<ShopProducts>();
    var updatedProducts = new List<ShopProducts>();

    foreach (var product in productsEntity)
    {
      var existingProduct = existingProducts
          .FirstOrDefault(p => p.ShopId == shopId && p.ProductId == product.ProductId);

      if (existingProduct != null)
      {
        existingProduct.Amount = product.Amount;
        existingProduct.Price = product.Price;
        updatedProducts.Add(existingProduct);
      }
      else
      {
        newProducts.Add(product);
      }
    }

    var allProducts = existingProducts
        .Where(p => p.ShopId != shopId)
        .Concat(newProducts)
        .Concat(updatedProducts)
        .ToList();

    await Storage.ShopProductsRepository.SaveProductsAsync(allProducts);

    return newProducts.Concat(updatedProducts).ToList();
  }

  public async Task<decimal> BuyProducts(int shopId, List<BuyRequestDTO> shopProducts)
  {
    var products = await Storage.ShopProductsRepository.LoadProductsAsync(shopId);
    decimal total = 0;

    foreach (var product in shopProducts)
    {
      var existingProduct = products
        .FirstOrDefault(p => p.ShopId == shopId && p.ProductId == product.Id);

      if (existingProduct == null)
        throw new Exception($"No product {product.Id} in shop {shopId}");

      if (product.Amount > existingProduct.Amount)
        throw new Exception($"Not enough products {product.Id} in shop {shopId}: {existingProduct.Amount}");

      if (existingProduct.Price < 0)
        throw new Exception($"Can't buy product {product.Id}");

      existingProduct.Amount -= product.Amount;
      total += product.Amount * existingProduct.Price;
    }

    await Storage.ShopProductsRepository.SaveProductsAsync(products);
    return total;
  }

  public async Task<PossibleProductsResponseDTO>
    PossibleProducts(int shopId, PossibleProductsRequestDTO possibleProductsRequest)
  {
    var products = await Storage.ShopProductsRepository.LoadProductsAsync(shopId);
    decimal maxBudget = possibleProductsRequest.Money;
    decimal currentBudget = maxBudget;

    var selectedProducts = new Dictionary<int, int>();

    foreach (var product in products.OrderBy(p => p.Price))
    {
      if (currentBudget <= 0) break;

      while (product.Amount > 0 && product.Price <= currentBudget)
      {
        if (!selectedProducts.ContainsKey(product.ProductId))
        {
          selectedProducts[product.ProductId] = 0;
        }

        selectedProducts[product.ProductId]++;
        currentBudget -= product.Price;
        product.Amount--;
      }
    }

    return new PossibleProductsResponseDTO { Products = selectedProducts };
  }
}
