using ShopOnline.Models;

namespace ShopOnline.WebApi.Repositories.IRepositories;

public interface IProductRepository
{
    Task<IReadOnlyList<Product>> GetProducts();
    Task<Product> GetProduct(int id);
    Task AddProduct(Product product);
    Task UpdateProduct(Product product);
    Task DeleteProduct(int id);
    Task<IReadOnlyList<ProductCategory>> GetCategories();
    Task<IReadOnlyList<CartItem>> GetCartItems();
    Task AddToCart(CartItem cartItem);
}