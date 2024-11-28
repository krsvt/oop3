
namespace DAL.Storage.Files;

public class FilesStorage : IStorage
{

    public IProductRepository ProductRepository { get; set; }
    public IShopRepository ShopRepository { get; set; }
    public IShopProductsRepository ShopProductsRepository { get; set; }

    public FilesStorage()
    {
        ProductRepository = new FilesProductRepository("product.json");
        ShopRepository = new FilesShopRepository("shop.json");
        ShopProductsRepository = new FilesShopProductsRepository("shopproducts.json");
    }


    public override string? ToString()
    {
        return "Files storage";
    }

}
