using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Storage.Database;

public class PostgreSQLShopStorage : IShopStorage
{
    private ShopsDbContext _context;

    public PostgreSQLShopStorage(ShopsDbContext context)
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
