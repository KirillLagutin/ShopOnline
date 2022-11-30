using ShopOnline.Domain.Entities;

namespace ShopOnline.Models.Responses;

public class RegisterResponse
{
    public string Message { get; set; }
    public Account Account { get; set; }
    public string Token { get; set; }
    
    public RegisterResponse(string? message = null)
    {
        Message = message;
    }
}