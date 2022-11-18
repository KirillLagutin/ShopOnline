using Microsoft.EntityFrameworkCore;
using ShopOnline.Models;
using ShopOnline.WebApi.Data;
using ShopOnline.WebApi.GenericRepository;
using ShopOnline.WebApi.Repositories.IRepositories;

namespace ShopOnline.WebApi.Repositories;

public class AccountRepository : EfRepository<Account>, IAccountRepository
{
    public AccountRepository(AppDbContext dbContext) : base(dbContext) {}
    
    public Task<Account?> FindByEmail(string email)
    {
        return _dbContext.Accounts.FirstOrDefaultAsync(a => 
            a.Email == email);
    }
}