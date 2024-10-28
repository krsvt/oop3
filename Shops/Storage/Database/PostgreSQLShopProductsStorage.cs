using Shops.Entities;
using Microsoft.EntityFrameworkCore;
using Shops.DTO;

namespace Shops.Storage.Database;

public class PostgreSQLShopProductsStorage : IShopProductsStorage
{
    private MyDbContext _context;

    public PostgreSQLShopProductsStorage(MyDbContext context)
    {
        _context = context;
    }

    async Task<List<ShopProducts>> IShopProductsStorage.GetProducts(int shopId)
    {
        return await _context.ShopProducts
            .Where(sp => sp.ShopId == shopId)
            .ToListAsync();
    }

    async Task<List<ShopProducts>>
        IShopProductsStorage.AddShopProducts
        (int shopId, List<ShopProducts> products)
    {
        var newProducts = new List<ShopProducts>();
        var updatedProducts = new List<ShopProducts>();
        // upsert in code
        foreach (var product in products)
        {
            var existingProduct = await _context.ShopProducts
                .FirstOrDefaultAsync(p => p.ShopId == shopId && p.ProductId == product.ProductId);
            if (existingProduct != null)
            {
                existingProduct.Amount = product.Amount;
                existingProduct.Price = product.Price;
                updatedProducts.Add(existingProduct);
            }
            else
            {
                newProducts.Add(product);
            }
        }
        await _context.ShopProducts.AddRangeAsync(newProducts);
        await _context.SaveChangesAsync();
        return newProducts.Concat(updatedProducts).ToList();
    }

    async Task<LowerProductPriceResponseDTO> IShopProductsStorage.LowerProductPrice(int productId)
    {
        var lowestPriceProduct = await _context.ShopProducts
            .Where(sp => sp.ProductId == productId)
            .OrderBy(sp => sp.Price)
            .FirstOrDefaultAsync();

        if (lowestPriceProduct == null)
        {
            return new LowerProductPriceResponseDTO
            {
                ShopId = 0,
                Price = 0
            };
        }

        return new LowerProductPriceResponseDTO
        {
            ShopId = lowestPriceProduct.ShopId,
            Price = lowestPriceProduct.Price
        };
    }

    Task<List<ShopProducts>> IShopProductsStorage.BuyProducts(int shopId, List<ShopProducts> ShopProducts)
    {
        throw new NotImplementedException();
    }


    Task<int> IShopProductsStorage.LowerShopProductsPrice(List<ShopProducts> ShopProducts)
    {
        throw new NotImplementedException();
    }

    Task<List<ShopProducts>> IShopProductsStorage.PossibleProducts(int shopId, decimal sum)
    {
        throw new NotImplementedException();
    }
}
