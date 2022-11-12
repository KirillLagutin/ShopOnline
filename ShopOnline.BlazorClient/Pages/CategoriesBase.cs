using Microsoft.AspNetCore.Components;
using ShopOnline.HttpApiClient;
using ShopOnline.Models;

namespace ShopOnline.BlazorClient.Pages;

public class CategoriesBase : ComponentBase
{
    [Inject]
    public IShopClient ShopClient { get; set; }

    public IEnumerable<ProductCategory> ProductCategories { get; set; }
    public IEnumerable<Product> Products { get; set; }
    
    protected override async Task OnInitializedAsync()
    {
        Products = await ShopClient.GetProducts();
        ProductCategories = await ShopClient.GetCategories();
    }
}