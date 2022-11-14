﻿using System.Net.Http.Json;
using ShopOnline.Models;

namespace ShopOnline.HttpApiClient;

public class ShopClient : IShopClient
{
    private const string DefaultHost = "https://localhost:7252";
    private const string DefaultController = "catalog";
    private readonly string _controller;
    private readonly string _host;
    private readonly HttpClient _httpClient;
    

    public ShopClient(
        string controller      = DefaultController, 
        string host            = DefaultHost, 
        HttpClient httpClient = null)
    {
        _controller = controller;
        _host       = host;
        _httpClient = httpClient ?? new HttpClient();
    }

// -------------------------  Products  ----------------------------
    public async Task<IReadOnlyList<Product>> GetProducts()
    {
        var uri = $"{_host}/{_controller}/get_products";
        var response = await _httpClient
            .GetFromJsonAsync<IReadOnlyList<Product>>(uri);
        
        return response;
    }

    public async Task<Product> GetProduct(int id)
    {
        var uri = $"{_host}/{_controller}/get_product";
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
        var uri = $"{_host}/{_controller}/add_product";
        await _httpClient.PostAsJsonAsync(uri, product);
    }

    public async Task<Product> UpdateProduct(int id)
    {
        var uri = $"{_host}/{_controller}/update_product";
        var product = await _httpClient
            .GetFromJsonAsync<Product>($"{uri}?id={id}");
        
        if (product is null)
        {
            throw new InvalidOperationException("Product can`t be null");
        }

        await _httpClient.PostAsJsonAsync(uri, product);

        return product;
    }

    public async Task DeleteProduct(int id)
    {
        var uri = $"{_host}/{_controller}/delete_product";
        var response = await _httpClient
            .PostAsync($"{uri}?id={id}", null);
        
        response.EnsureSuccessStatusCode();
    }

// -------------------------  Categories  ----------------------------
    public async Task<IReadOnlyList<ProductCategory>> GetCategories()
    {
        var uri = $"{_host}/{_controller}/get_categories";
        var response = await _httpClient
            .GetFromJsonAsync<IReadOnlyList<ProductCategory>>(uri);
        
        return response;
    }
    
// ----------------------------  Cart  -------------------------------
    public async Task<IReadOnlyList<CartItem>> GetCartItems()
    {
        var uri = $"{_host}/{_controller}/get_cartitems";
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
        var uri = $"{_host}/{_controller}/add_tocart";
        await _httpClient.PostAsJsonAsync(uri, cartItem);
    }
    
    public async Task DeleteFromCart(int id)
    {
        var uri = $"{_host}/{_controller}/delete_fromcart";
        var response = await _httpClient
            .PostAsync($"{uri}?id={id}", null);
        
        response.EnsureSuccessStatusCode();
    }
}