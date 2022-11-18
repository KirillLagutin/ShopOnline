namespace ShopOnline.Models;

public class ProductCategory : IEntity
{
    public Guid Id { get; init; }
    public string Name { get; set; }
    public string IconCss { get; set; }
}