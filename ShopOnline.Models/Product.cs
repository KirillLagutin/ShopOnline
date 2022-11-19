using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopOnline.Models;

public class Product : IEntity
{
    public Guid Id { get; init; }
    
    [Required(ErrorMessage = "Обязательное поле")]
    [StringLength(50, MinimumLength = 3, 
        ErrorMessage = "От 3 до 50 символов, пожалуйста")]
    public string Name { get; set; }

    public string Description { get; set; } = "Нет описания";
    public string ImageUrl { get; set; } = "/Images/NoPhoto.png";
    public decimal Price { get; set; }
    public int Quantity { get; set; } = 1;
    
    [Required(ErrorMessage = "Обязательное поле")]
    public Guid CategoryId { get; set; }
    
    [ForeignKey("CategoryId")]
    public ProductCategory? ProductCategory { get; set; }
}