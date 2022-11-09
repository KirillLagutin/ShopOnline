using Microsoft.EntityFrameworkCore;
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

    public async Task<IReadOnlyList<Product>> GetProducts()
        => await _appDbContext.Products.ToListAsync();

    public async Task<Product> GetProduct(long id)
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

    public async Task DeleteProduct(long id)
    {
        var product = await _appDbContext.Products
            .FirstAsync(p => p.Id == id);
        _appDbContext.Remove(product);
        await _appDbContext.SaveChangesAsync();
    }
}