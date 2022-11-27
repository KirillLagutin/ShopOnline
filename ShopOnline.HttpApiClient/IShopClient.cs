using ShopOnline.Models;
using ShopOnline.Models.Dto;

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
    Task RegisterAccount(AccountDto account);
    Task Authorization(AccountDto account);
}