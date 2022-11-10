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
        HttpClient? httpClient = null)
    {
        _controller = controller;
        _host       = host;
        _httpClient = httpClient ?? new HttpClient();
    }

    public async Task<IReadOnlyList<Product>> GetProducts()
    {
        var uri = $"{_host}/{_controller}/get_products";
        var response = await _httpClient
            .GetFromJsonAsync<IReadOnlyList<Product>>(uri);
        
        return response!;
    }

    public async Task<Product> GetProduct(long id)
    {
        var uri = $"{_host}/{_controller}/get_product";
        var product = await _httpClient
            .GetFromJsonAsync<Product>($"{uri}?productId={id}");
        
        if (product is null)
        {
            throw new InvalidOperationException("Product can`t be null");
        }

        return product;
    }
    
    public async Task AddProduct(Product product)
    {
        if (product is not null)
        {
            throw new ArgumentNullException(nameof(product));
        }
        var uri = $"{_host}/{_controller}/add_product";
        await _httpClient.PostAsJsonAsync(uri, product);
    }

    public async Task<Product> UpdateProduct(long id)
    {
        var uri = $"{_host}/{_controller}/update_product";
        var product = await _httpClient
            .GetFromJsonAsync<Product>($"{uri}?productId={id}");
        
        if (product is null)
        {
            throw new InvalidOperationException("Product can`t be null");
        }

        await _httpClient.PostAsJsonAsync(uri, product);

        return product;
    }

    public async Task DeleteProduct(long id)
    {
        var uri = $"{_host}/{_controller}/delete_product";
        var response = await _httpClient
            .PostAsync($"{uri}?productId={id}", null);
        
        response.EnsureSuccessStatusCode();
    }
}