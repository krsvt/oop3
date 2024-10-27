using Shops.Entities;
using Microsoft.EntityFrameworkCore;

namespace Shops.Storage.Database;

public class PostgreSQLShopStorage : IShopStorage
{
    private MyDbContext _context;

    public PostgreSQLShopStorage(MyDbContext context)
    {
        _context = context;
    }

    async Task<List<Shop>> IShopStorage.GetAllShops()
    {
        return await _context.Shops.ToListAsync();
    }

    async Task<Shop> IShopStorage.GetShop(int id)
    {
        return await _context.Shops.Where(p => p.Id == id).SingleAsync();
    }

    async Task IShopStorage.AddShop(Shop s)
    {
        await _context.Shops.AddAsync(s);
        await _context.SaveChangesAsync();
    }
}
