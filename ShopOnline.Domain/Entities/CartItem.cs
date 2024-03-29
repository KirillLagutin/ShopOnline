﻿using System.ComponentModel.DataAnnotations.Schema;
using ShopOnline.Domain.IGenericRepository;

namespace ShopOnline.Domain.Entities;

public class CartItem : IEntity
{
    public Guid Id { get; init; }
    public Guid CartId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    
    [ForeignKey("CartId")] 
    public Cart? Cart { get; set; }

    [ForeignKey("ProductId")] 
    public Product? Product { get; set; }
}