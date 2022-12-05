using System.ComponentModel.DataAnnotations;

namespace ShopOnline.HttpModels.Requests;

public class RegisterRequest
{
    [Required(ErrorMessage = "Обязательное поле")]
    [StringLength(24, MinimumLength = 3, 
        ErrorMessage = "От 3 до 30 символов, пожалуйста")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Обязательное поле")]
    [EmailAddress(ErrorMessage = "Некорректный Email")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Обязательное поле")]
    [StringLength(32, MinimumLength = 8, 
        ErrorMessage = "От 8 до 32 символов, пожалуйста")]
    public string Password { get; set; }
}