using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Storage.Database;

public class PostgreSQLProductStorage : IProductStorage
{
    private ShopsDbContext _context;

    public PostgreSQLProductStorage(ShopsDbContext context)
    {
        _context = context;
    }

    async Task<List<Product>> IProductStorage.GetAllProducts()
    {
        return await _context.Products.ToListAsync();
    }

    async Task<Product> IProductStorage.GetProduct(int id)
    {
        return await _context.Products.Where(p => p.Id == id).SingleAsync();
    }

    async Task IProductStorage.AddProduct(Product p)
    {
        await _context.Products.AddAsync(p);
        await _context.SaveChangesAsync();
    }
}
