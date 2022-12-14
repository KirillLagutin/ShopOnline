using ShopOnline.Domain.Entities;
using ShopOnline.HttpModels.Requests;
using ShopOnline.HttpModels.Responses;

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
    Task<RegisterResponse> RegisterAccount(RegisterRequest request);
    Task<LoginResponse> LogInAccount(LoginRequest account);
    Task<AccountInfoResponse> GetCurrentAccount();
    Task<Guid> GetIdByEmail(string email);
    Task DeleteAccount(Guid id);
}