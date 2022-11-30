using Microsoft.EntityFrameworkCore;
using ShopOnline.Domain.Entities;

namespace ShopOnline.Data.Ef.Data;

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