using System.Net.Http.Headers;
using System.Net.Http.Json;
using ShopOnline.Domain.Entities;
using ShopOnline.HttpModels.Requests;
using ShopOnline.HttpModels.Responses;

namespace ShopOnline.HttpApiClient;

public class ShopClient : IShopClient
{
    private const string DefaultHost = "https://localhost:7252";
    private readonly string _controller;
    private readonly string _host;
    private readonly HttpClient _httpClient;
    

    public ShopClient(
        string host            = DefaultHost, 
        HttpClient? httpClient = null)
    {
        _host       = host;
        _httpClient = httpClient ?? new HttpClient();
    }

    public void SetAuthorizationToken(string token)
    {
        if (token == null) throw new ArgumentNullException(nameof(token));
        var header = new AuthenticationHeaderValue("Bearer", token);
        _httpClient.DefaultRequestHeaders.Authorization = header;
        //IsAuthorizationTokenSet = true;
    }
    
// -------------------------  Products  ----------------------------
    public async Task<IReadOnlyList<Product>?> GetProducts()
    {
        var uri = $"{_host}/catalog/get_products";
        var response = await _httpClient
            .GetFromJsonAsync<IReadOnlyList<Product>>(uri);
        
        return response;
    }

    public async Task<Product?> GetProduct(Guid id)
    {
        var uri = $"{_host}/catalog/get_product";
        var product = await _httpClient
            .GetFromJsonAsync<Product>($"{uri}?id={id}");
        
        if (product is null)
        {
            throw new InvalidOperationException("Product can`t be null");
        }
        
        return product;
    }
    
    public async Task AddProduct(Product product)
    {
        if (product is null)
        {
            throw new ArgumentNullException(nameof(product));
        }
        var uri = $"{_host}/catalog/add_product";
        await _httpClient.PostAsJsonAsync(uri, product);
    }

    public async Task<Product> UpdateProduct(Guid id)
    {
        var uri = $"{_host}/catalog/update_product";
        var product = await _httpClient
            .GetFromJsonAsync<Product>($"{uri}?id={id}");
        
        if (product is null)
        {
            throw new InvalidOperationException("Product can`t be null");
        }

        await _httpClient.PostAsJsonAsync(uri, product);

        return product;
    }

    public async Task DeleteProduct(Guid id)
    {
        var uri = $"{_host}/catalog/delete_product";
        var response = await _httpClient
            .PostAsync($"{uri}?id={id}", null);
        
        response.EnsureSuccessStatusCode();
    }

// -------------------------  Categories  ----------------------------
    public async Task<IReadOnlyList<ProductCategory>?> GetCategories()
    {
        var uri = $"{_host}/category/get_categories";
        var response = await _httpClient
            .GetFromJsonAsync<IReadOnlyList<ProductCategory>>(uri);
        
        return response;
    }
    
// ----------------------------  Cart  -------------------------------
    public async Task<IReadOnlyList<CartItem>?> GetCartItems()
    {
        var uri = $"{_host}/cart/get_cartitems";
        var response = await _httpClient
            .GetFromJsonAsync<IReadOnlyList<CartItem>>(uri);
        
        return response;
    }
    
    public async Task AddToCart(CartItem cartItem)
    {
        if (cartItem is null)
        {
            throw new ArgumentNullException(nameof(cartItem));
        }
        var uri = $"{_host}/cart/add_cartitem";
        await _httpClient.PostAsJsonAsync(uri, cartItem);
    }
    
    public async Task DeleteCartItem(Guid id)
    {
        var uri = $"{_host}/cart/delete_cartitem";
        var response = await _httpClient
            .PostAsync($"{uri}?id={id}", null);
        
        response.EnsureSuccessStatusCode();
    }
    
    public async Task ClearCart()
    {

        var uri = $"{_host}/cart/clear_cart";
        var response = await _httpClient
            .PostAsync(uri, null);

        response.EnsureSuccessStatusCode();
    }
    
// ----------------------------  Account  -------------------------------
    public async Task<RegisterResponse> RegisterAccount(RegisterRequest request)
    {
        var uri = $"{_host}/account/register";
        var response = await _httpClient.PostAsJsonAsync(uri, request);

        response.EnsureSuccessStatusCode();
        
        var registerResponse = await response.Content.ReadFromJsonAsync<RegisterResponse>();

        return registerResponse!;
    }

    public async Task<LoginResponse> LogInAccount(LoginRequest account)
    {
        var uri = $"{_host}/account/login";
        var response = await _httpClient.PostAsJsonAsync(uri, account);

        response.EnsureSuccessStatusCode();
        
        var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
        
        SetAuthorizationToken(loginResponse!.Token);
        
        return loginResponse!;
    }
    
    public async Task<AccountInfoResponse> GetCurrentAccount()
    {
        var uri = $"{_host}/account/get_account";
        var response = await _httpClient
            .GetFromJsonAsync<AccountInfoResponse>(uri);

        return response!;
    }
    
    public async Task<Guid> GetIdByEmail(string email)
    {
        var uri = $"{_host}/account/get_id";
        var id = await _httpClient
            .GetFromJsonAsync<Guid>($"{uri}?email={email}");

        return id;
    }
    
    public async Task DeleteAccount(Guid id)
    {
        var uri = $"{_host}/account/delete_account";
        var response = await _httpClient
            .PostAsync($"{uri}?id={id}", null);
        
        response.EnsureSuccessStatusCode();
    }
}