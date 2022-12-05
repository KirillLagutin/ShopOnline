using ShopOnline.Domain.IGenericRepository;

namespace ShopOnline.Domain.Entities;

public class Account : IEntity
{
    public Guid Id { get; init; }
    public string? Name { get; set; }
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}