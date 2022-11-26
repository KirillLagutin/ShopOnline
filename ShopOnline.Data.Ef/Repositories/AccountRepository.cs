using Microsoft.EntityFrameworkCore;
using ShopOnline.Data.Ef.Data;
using ShopOnline.Data.Ef.GenericRepository;
using ShopOnline.Domain.IRepositories;
using ShopOnline.Models;

namespace ShopOnline.Data.Ef.Repositories;

public class AccountRepository : EfRepository<Account>, IAccountRepository
{
    public AccountRepository(AppDbContext dbContext) : base(dbContext) {}
    
    public Task<Account?> FindByEmail(string email)
    {
        return _dbContext.Accounts.FirstOrDefaultAsync(a => 
            a.Email == email);
    }
}