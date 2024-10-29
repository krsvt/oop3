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


    private bool ContainsAllProductsAndAmountIsFine(List<ShopProducts> shopProducts,
            List<BuyRequestDTO> requestedProducts)
    {
        var availableProducts = shopProducts.ToDictionary(sp => sp.ProductId, sp => sp.Amount);

        foreach (var product in requestedProducts)
        {
            if (!availableProducts.TryGetValue(product.Id, out var availableAmount))
            {
                return false;
            }

            if (availableAmount < product.Amount)
            {
                return false;
            }
        }

        return true;
    }

    async Task<LowerProductPriceResponseDTO> IShopProductsStorage.LowerShopProductsPrice(List<BuyRequestDTO> shopProducts)
    {
        var productIds = shopProducts.Select(sp => sp.Id).ToList();
        var requestedAmounts = shopProducts.ToDictionary(sp => sp.Id, sp => sp.Amount);

        var grouppedShops = await _context.ShopProducts
            .Where(sp => productIds.Contains(sp.ProductId))
            .GroupBy(sp => sp.ShopId)
            .ToListAsync();

        var temp = grouppedShops
            .Where(g =>
                    ContainsAllProductsAndAmountIsFine(g.ToList(), shopProducts)
                  )
            .Select(g => new
            {
                ShopId = g.Key,
                Price = g.Min(sp => sp.Price)
            }).ToList();

        var response = temp
            .OrderBy(gs => gs.Price)
            .FirstOrDefault();

        if (response == null)
        {
            throw new Exception("No suitable shop found");
        }

        return new LowerProductPriceResponseDTO { Price = response.Price, ShopId = response.ShopId };
    }

    async Task<PossibleProductsResponseDTO> IShopProductsStorage.PossibleProducts(int shopId, PossibleProductsRequestDTO request)
    {
        var all = await _context.ShopProducts
            .Where(s => s.Amount > 0)
            .OrderBy(s => s.Price)
            .ToListAsync();

        var max = request.Money;
        Console.WriteLine("max " + max);
        var current = max;
        var psIds = new Dictionary<int, int>();
        foreach (var p in all)
        {
            ShopProducts s = new ShopProducts { Price = p.Price, Amount = p.Amount, ProductId = p.ProductId, ShopId = shopId };
            Console.WriteLine("here 2");
            while (s.Amount > 0 && s.Price <= current)
            {
                current -= s.Price;
                s.Amount--;
                if (!psIds.ContainsKey(s.ProductId))
                {
                    psIds[s.ProductId] = 0;
                }
                psIds[s.ProductId]++;
                Console.WriteLine("here ");
            }
        }

        return new PossibleProductsResponseDTO { Products = psIds };
    }
}
