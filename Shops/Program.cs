using Shops.Configuration;
using Shops.Entities;
using Shops.Services;
using Shops.Storage;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

StorageConfiguration.SetStorage(builder.Configuration,
        builder.Services);

builder.Services.AddScoped<ProductService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var product = app.MapGroup("/api/product");

product.MapGet("/",
        async (
            IStorage st, ProductService p) =>
        {
            var pr = await p.GetAllProductsAsync();
            return Results.Ok(pr);
        })
.WithName("GetProducts")
.WithOpenApi();

product.MapGet("/{id}",
        async (int id,
            IStorage st, ProductService p) =>
        {
            var pr = await p.GetProductAsync(id);
            return Results.Ok(pr);
        })
.WithName("GetProductById")
.WithOpenApi();


product.MapPost("/",
        async (IStorage st, ProductService p, Product product) =>
        {
            await p.AddProductAsync(product);
            return Results.Created($"/api/product/{product.Id}", product);
        })
.WithName("AddProduct")
.WithOpenApi();

app.Run();
