﻿using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Runtime.CompilerServices;

namespace ShopOnline.Models;

public class Account : IEntity
{
    public Guid Id { get; init; }
    
    [Required(ErrorMessage = "Обязательное поле")]
    [StringLength(24, MinimumLength = 3, 
        ErrorMessage = "От 3 до 30 символов, пожалуйста")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Обязательное поле")]
    [EmailAddress(ErrorMessage = "Некорректный Email")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Обязательное поле")]
    [StringLength(32, MinimumLength = 6, 
        ErrorMessage = "От 6 до 32 символов, пожалуйста")]
    public string Password { get; set; }
}