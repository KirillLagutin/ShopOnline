using Microsoft.EntityFrameworkCore;
using ShopOnline.Data.Ef.Data;
using ShopOnline.Data.Ef.GenericRepository;
using ShopOnline.Domain.Entities;
using ShopOnline.Domain.IRepositories;

namespace ShopOnline.Data.Ef.Repositories;

public class AccountRepository : EfRepository<Account>, IAccountRepository
{
    public AccountRepository(AppDbContext dbContext) : base(dbContext) {}
    
    public async Task<Account?> FindByEmail(string email)
    {
        return await _dbContext.Accounts.FirstOrDefaultAsync(a => 
            a.Email == email);
    }
    
    public async Task<Account> GetAccount(Guid id)
    {
        var account = await _dbContext.Accounts
            .FirstAsync(a => a.Id == id);
        
        return account;
    }

    public async Task DeleteAccount(Guid id)
    {
        var account = await _dbContext.Accounts
            .FirstAsync(a => a.Id == id);
        _dbContext.Accounts.Remove(account);
        await _dbContext.SaveChangesAsync();
    }
}