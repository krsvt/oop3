using API.Configuration;
using DAL.Storage;
using DAL.Entities;
using BLL.Services;
using BLL.DTO;
using System.Text.Json;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers().AddJsonOptions(options =>
{
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});


builder.Configuration
       .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
       .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json",
                       optional: true, reloadOnChange: true)
       .AddEnvironmentVariables();

builder.Services.AddStorage(builder.Configuration);

builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<ShopService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
        app.UseSwagger();
        app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var shop = app.MapGroup("/api/shop");
var product = app.MapGroup("/api/product");


// GET http://localhost:5249/api/shop
shop.MapGet("/",
        async (IStorage st, ShopService s) =>
        {
                var shops = await s.GetAllShopsAsync();
                return Results.Ok(shops);
        })
.WithName("GetAllShops");

// GET http://localhost:5249/api/shop/1
shop.MapGet("/{id}",
        async (int id,
            IStorage st, ShopService s) =>
        {
                var shop = await s.GetShopAsync(id);
                return Results.Ok(shop);
        })
.WithName("GetShopById");

// POST http://localhost:5249/api/shop
shop.MapPost("/",
        async (IStorage st, ShopService s, Shop shop) =>
        {
                await s.AddShopAsync(shop);
                return Results.Created($"/api/shop/{shop.Id}", shop);
        })
.WithName("AddShop");

// GET http://localhost:5249/api/product
product.MapGet("/",
        async (
            IStorage st, ProductService p) =>
        {
                var pr = await p.GetAllProductsAsync();
                return Results.Ok(pr);
        })
.WithName("GetProducts")
.WithOpenApi();

// POST http://localhost:5249/api/product
product.MapPost("/",
        async (IStorage st, ProductService p, Product product) =>
        {
                await p.AddProductAsync(product);
                return Results.Created($"/api/product/{product.Id}", product);
        })
.WithName("AddProduct");

// GET http://localhost:5249/api/product/1
product.MapGet("/{id}",
        async (int id, IStorage st, ProductService p) =>
        {
                var pr = await p.GetProductAsync(id);
                return Results.Ok(pr);
        })
.WithName("GetProductById");

// POST http://localhost:5249/api/shop/1/add-products
shop.MapPost("/{shopId}/add-products",
        async (int shopId, IStorage st, ShopService s, List<AddProductsRequestDTO> shopProducts) =>
        {
                var added = await s.AddShopProductsAsync(shopId, shopProducts);
                return Results.Ok(added);
        })
.WithName("AddShopProducts");

// GET http://localhost:5249/api/product/1/lower-price
product.MapGet("/{productId}/lower-price",
        async (int productId, IStorage st, ProductService productService) =>
        {
                var lowerPriceShop = await productService.LowerProductPrice(productId);
                return Results.Ok(lowerPriceShop);
        })
.WithName("LowerPrice");

// POST http://localhost:5249/api/shop/1/buy
shop.MapPost("/{shopId}/buy",
        async (int shopId, IStorage st, ShopService s, List<BuyRequestDTO> shopProducts) =>
        {
                var total = await s.BuyProducts(shopId, shopProducts);
                return Results.Ok("total price = " + total);
        })
.WithName("BuyShopProducts");

// POST http://localhost:5249/api/product/lower-price-many
product.MapPost("lower-price-many",
        async (IStorage st, ProductService productService, List<BuyRequestDTO> shopProducts) =>
        {
                var totalResp = await productService.LowerShopProductsPrice(shopProducts);
                return Results.Ok(totalResp);
        })
.WithName("LowerPriceMany");

// POST http://localhost:5249/api/shop/1/possible-products
shop.MapPost("/{shopId}/possible-products",
        async (int shopId, IStorage st, ShopService s, PossibleProductsRequestDTO possibleProductsRequest) =>
        {
                Console.WriteLine("pos " + possibleProductsRequest);
                var products = await s.PossibleProducts(shopId, possibleProductsRequest);
                return Results.Ok(products);
        })
.WithName("PossibleProducts");

product.WithOpenApi();
shop.WithOpenApi();
app.Run();

