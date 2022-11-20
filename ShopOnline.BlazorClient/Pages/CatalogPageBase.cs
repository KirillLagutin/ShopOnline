using Microsoft.AspNetCore.Components;
using ShopOnline.HttpApiClient;
using ShopOnline.Models;

namespace ShopOnline.BlazorClient.Pages;

public class CatalogBase : ComponentBase
{
    [Inject]
    public IShopClient ShopClient { get; set; } = null!;

    [Inject]
    public NavigationManager? NavigationManager { get; set; }

    protected IEnumerable<Product>? Products { get; set; }
    private IEnumerable<ProductCategory>? ProductCategories { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Products = await ShopClient.GetProducts();
        ProductCategories = await ShopClient.GetCategories();
    }

    protected IEnumerable<IGrouping<Guid, Product>> GetGroupedProductByCategory()
    {
        return from    product in Products
               group   product by product.CategoryId into prodByCatGroup
               orderby ProductCategories
               select  prodByCatGroup;
    }

    protected string? GetCategoryName(IGrouping<Guid, Product> productGroup)
    {
        return ProductCategories?.First(pc =>
            pc.Id == productGroup.First().CategoryId).Name;
    }

    protected void NavigateToAddProduct()
    {
        NavigationManager?.NavigateTo("/ProductAdding");
    }

    protected void NavigateToDeleteProduct()
    {
        NavigationManager?.NavigateTo("/ProductDeleting");
    }
}