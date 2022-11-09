using ShopOnline.Models;

namespace ShopOnline.WebApi.Repositories.IRepositories;

public interface IProductRepository
{
    Task<IReadOnlyList<Product>> GetProducts();
    Task<Product> GetProduct(long id);
    Task AddProduct(Product product);
    Task UpdateProduct(Product product);
    Task DeleteProduct(long id);
}