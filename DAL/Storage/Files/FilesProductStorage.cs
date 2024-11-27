using DAL.Entities;
using System.Text.Json;

namespace DAL.Storage.Files;

public class FilesProductStorage : IProductStorage
{
    private readonly string _filePath;

    public FilesProductStorage(string filePath)
    {
        _filePath = filePath;
    }

    public async Task AddProduct(Product product)
    {
        var products = await GetAllProducts();
        product.Id = (products.Count > 0) ? products.Max(p => p.Id) + 1 : 1;
        products.Add(product);
        await SaveProductsToFileAsync(products);
    }

    public async Task<List<Product>> GetAllProducts()
    {
        if (!File.Exists(_filePath))
            return new List<Product>();

        var json = await File.ReadAllTextAsync(_filePath);
        return JsonSerializer.Deserialize<List<Product>>(json) ?? new List<Product>();
    }

    public async Task<Product> GetProduct(int id)
    {
        var products = await GetAllProducts();
        var product = products.FirstOrDefault(p => p.Id == id);

        if (product == null)
            throw new KeyNotFoundException($"Product with ID {id} not found.");

        return product;
    }

    private async Task SaveProductsToFileAsync(List<Product> products)
    {
        var json = JsonSerializer.Serialize(products);
        await File.WriteAllTextAsync(_filePath, json);
    }
}
