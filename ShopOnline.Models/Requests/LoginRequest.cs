using System.ComponentModel.DataAnnotations;

namespace ShopOnline.Models.Requests;

public class LoginRequest
{
    [EmailAddress]
    public string Email { get; set; }
    [MinLength(8)]
    public string Password { get; set; }
}