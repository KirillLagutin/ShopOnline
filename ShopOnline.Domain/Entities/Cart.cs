using System.ComponentModel.DataAnnotations.Schema;
using ShopOnline.Domain.IGenericRepository;

namespace ShopOnline.Domain.Entities;

public class Cart : IEntity
{
    public Guid Id { get; init; }
    public Guid AccountId { get; set; }
    
    [ForeignKey("AccountId")]
    public Account? Account { get; set; }
}