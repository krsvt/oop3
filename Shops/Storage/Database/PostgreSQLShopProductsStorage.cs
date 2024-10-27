using Shops.Entities;
using Microsoft.EntityFrameworkCore;

namespace Shops.Storage.Database;

public class PostgreSQLShopProductsStorage : IShopProductsStorage
{
    private MyDbContext _context;

    public PostgreSQLShopProductsStorage (MyDbContext context)
    {
        _context = context;
    }

    Task IShopProductsStorage.AddShopProducts(int shopId, List<ShopProducts> products)
    {
        throw new NotImplementedException();
    }

    Task<List<ShopProducts>> IShopProductsStorage.BuyProducts(int shopId, List<ShopProducts> ShopProducts)
    {
        throw new NotImplementedException();
    }

    Task<int> IShopProductsStorage.LowerProductPrice(int productId)
    {
        throw new NotImplementedException();
    }

    Task<int> IShopProductsStorage.LowerShopProductsPrice(List<ShopProducts> ShopProducts)
    {
        throw new NotImplementedException();
    }

    Task<List<ShopProducts>> IShopProductsStorage.PossibleProducts(int shopId, decimal sum)
    {
        throw new NotImplementedException();
    }
}
