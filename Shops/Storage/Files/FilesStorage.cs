
namespace Shops.Storage.Files;

public class FilesStorage : IStorage
{

    public IProductStorage ProductStorage { get; set; }
    public FilesStorage()
    {
        ProductStorage = new FilesProductStorage();
    }


    public override string? ToString()
    {
        return "Files storage";
    }

}
