using Shops.Entities;
using System.Text.Json;

namespace Shops.Storage.Files;

public class FilesShopProductsStorage : IShopProductsStorage
{
  private readonly string _filePath;

  public FilesShopProductsStorage(string filePath)
  {
    _filePath = filePath;
  }

  public async Task<List<ShopProducts>> LoadProductsAsync(int shopId)
  {
    if (!File.Exists(_filePath))
      return new List<ShopProducts>();

    var json = await File.ReadAllTextAsync(_filePath);
    var allProducts = JsonSerializer.Deserialize<List<ShopProducts>>(json)
      ?? new List<ShopProducts>();

    return allProducts.Where(p => p.ShopId == shopId).ToList();
  }


  public async Task SaveProductsAsync(List<ShopProducts> products)
  {
    var existingProducts = new List<ShopProducts>();
    if (File.Exists(_filePath))
    {
      var existingJson = await File.ReadAllTextAsync(_filePath);
      existingProducts = JsonSerializer.Deserialize<List<ShopProducts>>(existingJson)
        ?? new List<ShopProducts>();
    }

    foreach (var product in products)
    {
      var existingProduct = existingProducts.FirstOrDefault(p => p.ShopId == product.ShopId && p.ProductId == product.ProductId);
      if (existingProduct != null)
      {
        existingProduct.Amount = product.Amount;
        existingProduct.Price = product.Price;
      }
      else
      {
        existingProducts.Add(product);
      }
    }

    var json = JsonSerializer.Serialize(existingProducts,
        new JsonSerializerOptions { WriteIndented = true });
    await File.WriteAllTextAsync(_filePath, json);
  }

  public async Task<List<ShopProducts>> LoadProductsByProductIdsAsync(List<int> productIds)
  {
    if (!File.Exists(_filePath))
      return new List<ShopProducts>();

    var json = await File.ReadAllTextAsync(_filePath);
    var allProducts = JsonSerializer.Deserialize<List<ShopProducts>>(json) ?? new List<ShopProducts>();

    return allProducts
        .Where(sp => productIds.Contains(sp.ProductId))
        .ToList();
  }
}
