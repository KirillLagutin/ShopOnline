using ShopOnline.Models;

namespace ShopOnline.HttpApiClient;

public interface IShopClient
{
    Task<IReadOnlyList<Product>> GetProducts();
    Task<Product> GetProduct(int id);
    Task AddProduct(Product product);
    Task<Product> UpdateProduct(int id);
    Task DeleteProduct(int id);
    Task<IReadOnlyList<ProductCategory>> GetCategories();
    Task<IReadOnlyList<CartItem>> GetCartItems();
    Task AddToCart(CartItem cartItem);
}