using Microsoft.AspNetCore.Components;
using ShopOnline.HttpApiClient;
using ShopOnline.Models;

namespace ShopOnline.BlazorClient.Pages;

public class CatalogBase : ComponentBase
{
    [Inject]
    public IShopClient ShopClient { get; set; }

    public IEnumerable<Product> Products { get; set; } = null!;
    
    protected override async Task OnInitializedAsync()
    {
        Products = await ShopClient.GetProducts();
    }
}