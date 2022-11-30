using Microsoft.AspNetCore.Mvc;
using ShopOnline.Domain.Entities;
using ShopOnline.Domain.IGenericRepository;

namespace ShopOnline.WebApi.Controllers;

[ApiController]
[Route("cart")]
public class CartController : ControllerBase
{
    private readonly IRepository<CartItem> _cartItems;

    public CartController(IRepository<CartItem> cartItems)
    {
        _cartItems = cartItems;
    }
    
    [HttpGet("get_cartitems")]
    public async Task<ActionResult<IEnumerable<CartItem>>> GetCartItems()
    {
        var cartItems = await _cartItems.GetAll();
        return Ok(cartItems);
    }
    
    [HttpPost("add_tocart")]
    public async Task<ActionResult> AddToCart(CartItem cartItem)
    {
        await _cartItems.Add(cartItem);
        return Ok(cartItem);
    }
    
    [HttpPost("delete_cartitem")]
    public async Task<ActionResult> DeleteCartItemById(Guid id)
    {
        await _cartItems.DeleteById(id);
        return Ok();
    }
    
    [HttpPost("clear_cart")]
    public async Task<ActionResult> ClearCartItems()
    {
        await _cartItems.ClearItems();
        return Ok();
    }
}