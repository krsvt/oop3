using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Storage.Database.Repository;

public class DatabaseShopRepository : IShopRepository
{
    private ShopsDbContext _context;

    public DatabaseShopRepository(ShopsDbContext context)
    {
        _context = context;
    }

    async Task<List<Shop>> IShopRepository.GetAllShops()
    {
        return await _context.Shops.ToListAsync();
    }

    async Task<Shop> IShopRepository.GetShop(int id)
    {
        return await _context.Shops.Where(p => p.Id == id).SingleAsync();
    }

    async Task IShopRepository.AddShop(Shop s)
    {
        await _context.Shops.AddAsync(s);
        await _context.SaveChangesAsync();
    }
}
