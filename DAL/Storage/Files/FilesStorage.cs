using DAL.Entities;

namespace DAL.Storage.Files;

public class FilesStorage : IStorage
{
    public IRepository<Product> ProductRepository { get; set; }
    public IRepository<Shop> ShopRepository { get; set; }
    public IRepository<ShopProducts> ShopProductsRepository { get; set; }

    public FilesStorage()
    {
        ProductRepository = new FilesRepository<Product>("product.json");
        ShopRepository = new FilesRepository<Shop>("shop.json");
        ShopProductsRepository = new FilesRepository<ShopProducts>("shopproducts.json");
    }

    public override string? ToString()
    {
        return "Files storage";
    }
}
