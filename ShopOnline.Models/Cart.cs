using System.ComponentModel.DataAnnotations.Schema;

namespace ShopOnline.Models;

public class Cart : IEntity
{
    public Guid Id { get; init; }
    public Guid AccountId { get; set; }
    
    [ForeignKey("AccountId")]
    public Account Account { get; set; }
}