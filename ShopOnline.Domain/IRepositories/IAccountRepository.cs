using ShopOnline.Domain.IGenericRepository;
using ShopOnline.Models;

namespace ShopOnline.Domain.IRepositories;

public interface IAccountRepository : IRepository<Account>
{
    Task<Account?> FindByEmail(string email);
}