using Microsoft.AspNetCore.Mvc;
using ShopOnline.Models;
using ShopOnline.WebApi.GenericRepository.IGenericRepository;

namespace ShopOnline.WebApi.Controllers;

[ApiController]
[Route("catalog")]
public class ProductController : ControllerBase
{
    private readonly IRepository<Product> _products;

    public ProductController(IRepository<Product> products)
    {
        _products = products;
    }
    
    [HttpGet("get_products")]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        var products =  await _products.GetAll();
        return Ok(products);
    }
    
    [HttpGet("get_product")]
    public async Task<ActionResult<Product>> GetProduct(Guid id)
    {
        var product = await _products.GetById(id);
        return Ok(product);
    }
    
    [HttpPost("add_product")]
    public async Task<ActionResult> AddProduct(Product product)
    {
        await _products.Add(product);
        return Ok(product);
    }
    
    [HttpPost("update_product")]
    public async Task<ActionResult> UpdateProduct(Product product)
    {
        await _products.Update(product);
        return Ok(product);
    }
    
    [HttpPost("delete_product")]
    public async Task<ActionResult> DeleteProduct(Guid id)
    {
        await _products.DeleteById(id);
        return Ok();
    }
}