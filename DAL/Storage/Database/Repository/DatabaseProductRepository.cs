using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Storage.Database.Repository;

public class DatabaseProductRepository : IProductRepository
{
    private ShopsDbContext _context;

    public DatabaseProductRepository(ShopsDbContext context)
    {
        _context = context;
    }

    async Task<List<Product>> IProductRepository.GetAllProducts()
    {
        return await _context.Products.ToListAsync();
    }

    async Task<Product> IProductRepository.GetProduct(int id)
    {
        return await _context.Products.Where(p => p.Id == id).SingleAsync();
    }

    async Task IProductRepository.AddProduct(Product p)
    {
        await _context.Products.AddAsync(p);
        await _context.SaveChangesAsync();
    }
}
