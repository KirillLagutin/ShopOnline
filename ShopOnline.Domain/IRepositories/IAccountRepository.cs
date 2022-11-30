using ShopOnline.Domain.Entities;
using ShopOnline.Domain.IGenericRepository;

namespace ShopOnline.Domain.IRepositories;

public interface IAccountRepository : IRepository<Account>
{
    Task<Account?> FindByEmail(string email);
    Task<Account> GetAccount(Guid id);
    Task DeleteAccount(Guid id);
}