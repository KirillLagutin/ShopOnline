namespace ShopOnline.HttpModels.Responses;

public class RegisterResponse
{
    public string Message { get; set; }
    public string Token { get; set; }
    
    public RegisterResponse(string? message = null)
    {
        Message = message;
    }
}