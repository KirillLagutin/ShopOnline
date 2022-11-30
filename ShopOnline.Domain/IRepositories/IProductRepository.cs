using ShopOnline.Domain.Entities;

namespace ShopOnline.Domain.IRepositories;

public interface IProductRepository
{
    Task<IReadOnlyList<Product>> GetProducts();
    Task<Product> GetProduct(Guid id);
    Task AddProduct(Product product);
    Task UpdateProduct(Product product);
    Task DeleteProduct(Guid id);
    Task<IReadOnlyList<ProductCategory>> GetCategories();
    Task<ProductCategory> GetCategory(Guid id);
    Task AddCategory(ProductCategory categories);
    Task<IReadOnlyList<CartItem>> GetCartItems();
    Task AddToCart(CartItem cartItem);
    Task DeleteCartItem(Guid id);
    Task ClearCart();
}