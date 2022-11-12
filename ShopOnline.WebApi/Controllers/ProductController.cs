using Microsoft.AspNetCore.Mvc;
using ShopOnline.Models;
using ShopOnline.WebApi.Repositories.IRepositories;

namespace ShopOnline.WebApi.Controllers;

[Route("catalog")]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public ProductController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    
// -------------------------  Products  ----------------------------
    [HttpGet("get_products")]
    public async Task<IEnumerable<Product>> GetProducts()
    {
        return await _productRepository.GetProducts();
    }
    
    [HttpGet("get_product")]
    public async Task <Product> GetProduct(int id)
    {
        return await _productRepository.GetProduct(id);
    }
    
    [HttpPost("add_product")]
    public async Task AddProduct(Product product)
    {
        await _productRepository.AddProduct(product);
    }
    
    [HttpPost("update_product")]
    public async Task UpdateProduct(Product product)
    {
        await _productRepository.UpdateProduct(product);
    }
    
    [HttpPost("delete_product")]
    public async Task DeleteProduct(int id)
    {
        await _productRepository.DeleteProduct(id);
    }
    
// -------------------------  Categories  ----------------------------
    [HttpGet("get_categories")]
    public async Task<IEnumerable<ProductCategory>> GetCategories()
    {
        return await _productRepository.GetCategories();
    }
    
    [HttpGet("get_cartitems")]
    public async Task<IEnumerable<CartItem>> GetCartItems()
    {
        return await _productRepository.GetCartItems();
    }
    
// ---------------------------  Cart  -------------------------------
    [HttpPost("add_tocart")]
    public async Task AddToCart(CartItem cartItem)
    {
        await _productRepository.AddToCart(cartItem);
    }
}