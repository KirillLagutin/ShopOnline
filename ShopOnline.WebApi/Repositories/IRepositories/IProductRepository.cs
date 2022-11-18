using ShopOnline.Models;
using ShopOnline.WebApi.GenericRepository.IGenericRepository;

namespace ShopOnline.WebApi.Repositories.IRepositories;

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
}