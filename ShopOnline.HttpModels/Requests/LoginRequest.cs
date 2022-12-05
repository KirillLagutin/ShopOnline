using System.ComponentModel.DataAnnotations;

namespace ShopOnline.HttpModels.Requests;

public class LoginRequest
{
    [EmailAddress]
    public string Email { get; set; }
    [MinLength(8)]
    public string Password { get; set; }
}