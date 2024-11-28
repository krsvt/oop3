using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Storage.Database.Repository;

public class DatabaseShopProductsRepository : IShopProductsRepository
{
    private ShopsDbContext _context;

    public DatabaseShopProductsRepository(ShopsDbContext context)
    {
        _context = context;
    }

    async Task<List<ShopProducts>> IShopProductsRepository.LoadProductsAsync(int shopId)
    {
        return await _context.ShopProducts.Where(p => p.ShopId == shopId).ToListAsync();
    }

    async Task IShopProductsRepository.SaveProductsAsync(List<ShopProducts> products)
    {
        _context.ShopProducts.UpdateRange(products);
        await _context.SaveChangesAsync();
    }

    async Task<List<ShopProducts>> IShopProductsRepository.LoadProductsByProductIdsAsync(List<int> productIds)
    {
        return await _context.ShopProducts
            .Where(sp => productIds.Contains(sp.ProductId))
            .ToListAsync();
    }

}
