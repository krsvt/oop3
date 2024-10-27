using Shops.Entities;

namespace Shops.Storage.Files;

public class FilesShopStorage : IShopStorage
{
    Task IShopStorage.AddShop(Shop s)
    {
        throw new NotImplementedException();
    }

    Task<List<Shop>> IShopStorage.GetAllShops()
    {
        throw new NotImplementedException();
    }

    Task<Shop> IShopStorage.GetShop(int id)
    {
        throw new NotImplementedException();
    }
}
