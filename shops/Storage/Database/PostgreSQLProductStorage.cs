using shops.Entities;

namespace shops.Storage.Database;

public class PostgreSQLProductStorage : IProductStorage
{
    private MyDbContext _context;

    public PostgreSQLProductStorage (MyDbContext context) {
        _context = context;
    }
    Product IProductStorage.AddProduct(Product p)
    {
        throw new NotImplementedException();
    }

    Product IProductStorage.GetAllProducts()
    {
        throw new NotImplementedException();
    }

    Product IProductStorage.GetProduct(int id)
    {
        throw new NotImplementedException();
    }
}
