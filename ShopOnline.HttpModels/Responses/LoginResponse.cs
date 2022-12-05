namespace ShopOnline.HttpModels.Responses;

public class LoginResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
}