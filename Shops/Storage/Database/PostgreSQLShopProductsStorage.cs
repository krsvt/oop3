using Shops.Entities;
using Microsoft.EntityFrameworkCore;

namespace Shops.Storage.Database;

public class PostgreSQLShopProductsStorage : IShopProductsStorage
{
    private MyDbContext _context;

    public PostgreSQLShopProductsStorage(MyDbContext context)
    {
        _context = context;
    }

    async Task<List<ShopProducts>> IShopProductsStorage.LoadProductsAsync(int shopId)
    {
        return await _context.ShopProducts.Where(p => p.ShopId == shopId).ToListAsync();
    }

    async Task IShopProductsStorage.SaveProductsAsync(List<ShopProducts> products)
    {
        _context.ShopProducts.UpdateRange(products);
        await _context.SaveChangesAsync();
    }

    async Task<List<ShopProducts>> IShopProductsStorage.LoadProductsByProductIdsAsync(List<int> productIds)
    {
        return await _context.ShopProducts
            .Where(sp => productIds.Contains(sp.ProductId))
            .ToListAsync();
    }

}
