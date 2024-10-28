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

    async Task<decimal> IShopProductsStorage.BuyProducts(int shopId, List<BuyRequestDTO> ShopProducts)
    {
        decimal total = 0;
        foreach (var product in ShopProducts)
        {
            var p = await _context.ShopProducts
                .Where(s => s.ShopId == shopId && s.ProductId == product.Id).FirstOrDefaultAsync();

            if (p == null)
                throw new Exception($"No product ${product.Id} in shop {shopId}");

            if (product.Amount > p.Amount)
                throw new Exception($"Not enough products ${product.Id} in shop {shopId}: {p.Amount}");

            if (p.Price < 0)
                throw new Exception($"Can't buy product {product.Id}");
            p.Amount -= product.Amount;
            await _context.SaveChangesAsync();
            total += product.Amount * p.Price;
        }
        return total;
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
