using Microsoft.EntityFrameworkCore;
using ShopOnline.BlazorClient.Pages;
using ShopOnline.Models;
using ShopOnline.WebApi.Data;
using ShopOnline.WebApi.Repositories.IRepositories;

namespace ShopOnline.WebApi.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _appDbContext;

    public ProductRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

// -------------------------  Products  ----------------------------
    public async Task<IReadOnlyList<Product>> GetProducts()
        => await _appDbContext.Products.ToListAsync();

    public async Task<Product> GetProduct(Guid id)
        => await _appDbContext.Products.FirstAsync(p => p.Id == id);

    public async Task AddProduct(Product product)
    {
        await _appDbContext.Products.AddAsync(product);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task UpdateProduct(Product product)
    {
        _appDbContext.Entry(product).State = EntityState.Modified;
        await _appDbContext.SaveChangesAsync();
    }
    
    public async Task DeleteProduct(Guid id)
    {
        var product = await _appDbContext.Products
            .FirstAsync(p => p.Id == id);
        _appDbContext.Remove(product);
        await _appDbContext.SaveChangesAsync();
    }
    
// -------------------------  Categories  ----------------------------
    public async Task<IReadOnlyList<ProductCategory>> GetCategories()
        => await _appDbContext.ProductCategories.ToListAsync();
    
    public async Task<ProductCategory> GetCategory(Guid id)
        => await _appDbContext.ProductCategories
            .FirstAsync(p => p.Id == id);
    
    public async Task AddCategory(ProductCategory categories)
    {
        await _appDbContext.ProductCategories.AddAsync(categories);
        await _appDbContext.SaveChangesAsync();
    }
    
// ----------------------------  Cart  -------------------------------
    public async Task<IReadOnlyList<CartItem>> GetCartItems()
        => await _appDbContext.CartItems.ToListAsync();
    
    public async Task AddToCart(CartItem cartItem)
    {
        await _appDbContext.CartItems.AddAsync(cartItem);
        await _appDbContext.SaveChangesAsync();
    }
}