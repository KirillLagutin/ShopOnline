using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopOnline.Models;
using ShopOnline.WebApi.Data;
using ShopOnline.WebApi.Repositories;
using ShopOnline.WebApi.Repositories.IRepositories;

var builder = WebApplication.CreateBuilder(args);

var dbPath = builder.Configuration["DbPath"];


// Add services to the container.

// Connection to db
builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseSqlite($"Data Source={dbPath}"));

// Add CORS
builder.Services.AddCors();

// Registering repository
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddControllers();

// Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// CORS
app.UseCors(policy => policy
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => 
        origin is "https://localhost:7051" 
            or "http://localhost:5108")
    .AllowCredentials()
);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// GET PRODUCTS
app.MapGet("/get_products",
    async ([FromServices] IProductRepository productRepository) =>
    {
        var products = await productRepository.GetProducts();

        return products == null 
            ? Results.NotFound(new {message = "Товар не найден"}) 
            : Results.Ok(products);
    }
);

// GET PRODUCT
app.MapGet("/get_product",
    async ([FromServices] IProductRepository productRepository,
        [FromQuery] long productId) =>
    {
        var product = await productRepository.GetProduct(productId);
        
        return product == null 
            ? Results.NotFound(new {message = "Товар не найден"}) 
            : Results.Ok(product);
    }
);

// ADD PRODUCT
app.MapPost("/add_product",
    async ([FromServices] IProductRepository productRepository,
        [FromBody] Product product) =>
    {
        if (product == null)
        {
            return Results.NotFound(new {message = "Товар не найден"});
        }

        await productRepository.AddProduct(product);
        return Results.Ok(product);
    }
);

// UPDATE PRODUCT
app.MapPost("/update_product",
    async ([FromServices] IProductRepository productRepository,
        [FromBody] Product product) =>
    {
        if (product == null)
        {
            return Results.NotFound(new {message = "Товар не найден"});
        }

        await productRepository.UpdateProduct(product);
        return Results.Ok(product);
    }
);

// DELETE PRODUCT
app.MapPost("/delete_product",
    async ([FromServices] IProductRepository productRepository,
        [FromQuery] long productId) =>
    {
        await productRepository.DeleteProduct(productId);
        return Results.Ok();
    }
);

app.Run();