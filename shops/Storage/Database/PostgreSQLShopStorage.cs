using shops.Entities;

namespace shops.Storage.Database;

public class PostgreSQLShopStorage : IShopStorage
{
    private MyDbContext _context;

    public PostgreSQLShopStorage (MyDbContext context) {
        _context = context;
    }

    Shop IShopStorage.AddShop(Product p)
    {
        throw new NotImplementedException();
    }

    Shop IShopStorage.GetAllShops()
    {
        throw new NotImplementedException();
    }

    Shop IShopStorage.GetShop(int id)
    {
        throw new NotImplementedException();
    }
}
