
namespace Shops.Storage.Database;

public class PostgreSQLStorage : IStorage
{

    public IProductStorage ProductStorage { get; set; }
    public IShopStorage ShopStorage { get; set; }

    public PostgreSQLStorage(MyDbContext context)
    {
        ProductStorage = new PostgreSQLProductStorage(context);
        ShopStorage = new PostgreSQLShopStorage(context);
    }

    public override string? ToString()
    {
        return "PostgreSQL storage";
    }
}
