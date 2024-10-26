
namespace shops.Storage.Database;

public class PostgreSQLStorage : IStorage
{

    public IProductStorage ProductStorage { get; set; }
    public PostgreSQLStorage(MyDbContext context)
    {
        ProductStorage = new PostgreSQLProductStorage(context);
    }

    public override string? ToString()
    {
        return "PostgreSQL storage";
    }
}
