using Shops.Entities;
using Microsoft.EntityFrameworkCore;

namespace Shops.Storage.Database;

public class PostgreSQLProductStorage : IProductStorage
{
    private MyDbContext _context;

    public PostgreSQLProductStorage(MyDbContext context)
    {
        _context = context;
    }

    async Task<List<Product>> IProductStorage.GetAllProducts()
    {
        return await _context.Products.ToListAsync();
    }

    Task<Product> IProductStorage.AddProduct(Product p)
    {
        throw new NotImplementedException();
    }


    Task<Product> IProductStorage.GetProduct(int id)
    {
        throw new NotImplementedException();
    }
}
