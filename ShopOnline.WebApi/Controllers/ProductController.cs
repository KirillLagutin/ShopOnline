using Microsoft.AspNetCore.Mvc;
using ShopOnline.Models;
using ShopOnline.WebApi.GenericRepository.IGenericRepository;
using ShopOnline.WebApi.Repositories.IRepositories;

namespace ShopOnline.WebApi.Controllers;

[Route("catalog")]
public class ProductController : ControllerBase
{
    private readonly IRepository<Product> _product;
    private readonly IRepository<ProductCategory> _category;

    public ProductController(IRepository<Product> product,
        IRepository<ProductCategory> category)
    {
        _product = product;
        _category = category;
    }
    
// -------------------------  Products  ----------------------------
    [HttpGet("get_products")]
    public async Task<IEnumerable<Product>> GetProducts()
    {
        return await _product.GetAll();
    }
    
    [HttpGet("get_product")]
    public async Task <Product> GetProduct(Guid id)
    {
        return await _product.GetById(id);
    }
    
    [HttpPost("add_product")]
    public async Task AddProduct(Product product)
    {
        await _product.Add(product);
    }
    
    [HttpPost("update_product")]
    public async Task UpdateProduct(Product product)
    {
        await _product.Update(product);
    }
    
    [HttpPost("delete_product")]
    public async Task DeleteProduct(Guid id)
    {
        await _product.DeleteById(id);
    }
    
// -------------------------  Categories  ----------------------------
    [HttpGet("get_categories")]
    public async Task<IReadOnlyList<ProductCategory>> GetCategories()
    {
        return await _category.GetAll();
    }
    
    [HttpGet("get_category")]
    public async Task <ProductCategory> GetCategory(Guid id)
    {
        return await _category.GetById(id);
    }
    
    [HttpPost("add_category")]
    public async Task AddCategory(ProductCategory category)
    {
        await _category.Add(category);
    }
    
// ---------------------------  Cart  -------------------------------
    // [HttpGet("get_cartitems")]
    // public async Task<IEnumerable<CartItem>> GetCartItems()
    // {
    //     return await _product.GetCartItems();
    // }
    //
    // [HttpPost("add_tocart")]
    // public async Task AddToCart(CartItem cartItem)
    // {
    //     await _product.AddToCart(cartItem);
    // }
}