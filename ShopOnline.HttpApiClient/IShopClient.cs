using ShopOnline.Domain.Entities;
using ShopOnline.Models.Requests;
using ShopOnline.Models.Responses;

namespace ShopOnline.HttpApiClient;

public interface IShopClient
{
    Task<IReadOnlyList<Product>?> GetProducts();
    Task<Product?> GetProduct(Guid id);
    Task AddProduct(Product product);
    Task<Product> UpdateProduct(Guid id);
    Task DeleteProduct(Guid id);
    Task<IReadOnlyList<ProductCategory>?> GetCategories();
    Task<IReadOnlyList<CartItem>?> GetCartItems();
    Task AddToCart(CartItem cartItem);
    Task DeleteCartItem(Guid id);
    Task ClearCart();
    Task RegisterAccount(RegisterRequest request);
    Task LogInAccount(LoginRequest account);
    Task<Account?> GetAccountById(Guid id);
    Task<Guid> GetIdByEmail(string email);
    Task DeleteAccount(Guid id);
}