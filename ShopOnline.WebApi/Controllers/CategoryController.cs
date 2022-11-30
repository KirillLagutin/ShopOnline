using Microsoft.AspNetCore.Mvc;
using ShopOnline.Domain.Entities;
using ShopOnline.Domain.IGenericRepository;

namespace ShopOnline.WebApi.Controllers;

[ApiController]
[Route("category")]
public class CategoryController : ControllerBase
{
    private readonly IRepository<ProductCategory> _categories;

    public CategoryController(IRepository<ProductCategory> categories)
    {
        _categories = categories;
    }
    
    [HttpGet("get_categories")]
    public async Task<ActionResult<IEnumerable<ProductCategory>>> GetCategories()
    {
        var categories =  await _categories.GetAll();
        return Ok(categories);
    }
    
    [HttpGet("get_category")]
    public async Task<ActionResult<ProductCategory>> GetCategory(Guid id)
    {
        var category = await _categories.GetById(id);
        return Ok(category);
    }
    
    [HttpPost("add_category")]
    public async Task<ActionResult> AddCategory(ProductCategory category)
    {
        await _categories.Add(category);
        return Ok();
    }
}