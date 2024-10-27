
namespace Shops.Storage.Files;

public class FilesStorage : IStorage
{

    public IProductStorage ProductStorage { get; set; }
    public IShopStorage ShopStorage { get; set; }
    public IShopProductsStorage ShopProductsStorage { get; set; }

    public FilesStorage()
    {
        ProductStorage = new FilesProductStorage();
        ShopStorage = new FilesShopStorage();
        ShopProductsStorage = new FilesShopProductsStorage();
    }


    public override string? ToString()
    {
        return "Files storage";
    }

}
