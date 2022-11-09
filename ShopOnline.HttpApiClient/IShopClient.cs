using ShopOnline.Models;

namespace ShopOnline.HttpApiClient;

public interface IShopClient
{
    Task<IReadOnlyList<Product>> GetProducts();
    Task<Product> GetProduct(long id);
    Task AddProduct(Product product);
    Task<Product> UpdateProduct(long id);
    Task DeleteProduct(long id);
}