
namespace Shops.Storage.Files;

public class FilesStorage : IStorage
{

    public IProductStorage ProductStorage { get; set; }
    public IShopStorage ShopStorage { get; set; }
    public IShopProductsStorage ShopProductsStorage { get; set; }

    public FilesStorage()
    {
        ProductStorage = new FilesProductStorage("product.json");
        ShopStorage = new FilesShopStorage("shop.json");
        ShopProductsStorage = new FilesShopProductsStorage("shopproducts.json");
    }


    public override string? ToString()
    {
        return "Files storage";
    }

}
