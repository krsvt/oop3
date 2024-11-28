using DAL.Storage.Database.Repository;
namespace DAL.Storage.Database;

public class DatabaseStorage : IStorage
{

    public IProductRepository ProductRepository { get; set; }
    public IShopRepository ShopRepository { get; set; }
    public IShopProductsRepository ShopProductsRepository { get; set; }

    public DatabaseStorage(ShopsDbContext context)
    {
        ProductRepository = new DatabaseProductRepository(context);
        ShopRepository = new DatabaseShopRepository(context);
        ShopProductsRepository = new DatabaseShopProductsRepository(context);
    }

    public override string? ToString()
    {
        return "Database storage";
    }
}
