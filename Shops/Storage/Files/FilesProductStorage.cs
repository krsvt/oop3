using Shops.Entities;

namespace Shops.Storage.Files;

public class FilesProductStorage : IProductStorage
{
    Task<Product> IProductStorage.AddProduct(Product p)
    {
        throw new NotImplementedException();
    }

    Task<List<Product>> IProductStorage.GetAllProducts()
    {
        throw new NotImplementedException();
    }

    Task<Product> IProductStorage.GetProduct(int id)
    {
        throw new NotImplementedException();
    }
}
