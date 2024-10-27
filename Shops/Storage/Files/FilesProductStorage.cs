using Shops.Entities;

namespace Shops.Storage.Files;

public class FilesProductStorage : IProductStorage
{
    Task<List<Product>> IProductStorage.GetAllProducts()
    {
        throw new NotImplementedException();
    }

    Task<Product> IProductStorage.GetProduct(int id)
    {
        throw new NotImplementedException();
    }

    Task IProductStorage.AddProduct(Product p)
    {
        throw new NotImplementedException();
    }

}
