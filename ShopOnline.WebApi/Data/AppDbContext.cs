using Microsoft.EntityFrameworkCore;
using ShopOnline.Models;

namespace ShopOnline.WebApi.Data;

public class AppDbContext : DbContext
{
	public DbSet<Product> Products => Set<Product>();
	public DbSet<ProductCategory> ProductCategories => Set<ProductCategory>();
	public DbSet<Cart> Carts => Set<Cart>();
 	public DbSet<CartItem> CartItems => Set<CartItem>();
    public DbSet<Account> Accounts => Set<Account>();
	
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
	    modelBuilder.Entity<Account>().HasIndex(b => b.Email)
		    .IsUnique();
    }
}