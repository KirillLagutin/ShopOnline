using System.ComponentModel.DataAnnotations.Schema;

namespace ShopOnline.Models;

public class Product : IEntity
{
    public Guid Id { get; init; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public Guid? CategoryId { get; set; }
    
    [ForeignKey("CategoryId")]
    public ProductCategory? ProductCategory { get; set; }
}