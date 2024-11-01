using Shops.Entities;
using System.Text.Json;

namespace Shops.Storage.Files;

public class FilesShopStorage : IShopStorage
{
    private readonly string _filePath;

    public FilesShopStorage(string filePath)
    {
        _filePath = filePath;
    }

    public async Task AddShop(Shop shop)
    {
        var shops = await GetAllShops();
        shop.Id = (shops.Count > 0) ? shops.Max(s => s.Id) + 1 : 1;
        shops.Add(shop);
        await SaveShopsToFileAsync(shops);
    }

    public async Task<List<Shop>> GetAllShops()
    {
        if (!File.Exists(_filePath))
            return new List<Shop>();

        var json = await File.ReadAllTextAsync(_filePath);
        return JsonSerializer.Deserialize<List<Shop>>(json) ?? new List<Shop>();
    }

    public async Task<Shop> GetShop(int id)
    {
        var shops = await GetAllShops();
        var shop = shops.FirstOrDefault(s => s.Id == id);

        if (shop == null)
            throw new KeyNotFoundException($"Shop with ID {id} not found.");

        return shop;
    }

    private async Task SaveShopsToFileAsync(List<Shop> shops)
    {
        var json = JsonSerializer.Serialize(shops);
        await File.WriteAllTextAsync(_filePath, json);
    }
}
