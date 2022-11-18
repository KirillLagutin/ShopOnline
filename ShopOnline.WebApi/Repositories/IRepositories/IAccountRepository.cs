using ShopOnline.Models;
using ShopOnline.WebApi.GenericRepository.IGenericRepository;

namespace ShopOnline.WebApi.Repositories.IRepositories;

public interface IAccountRepository : IRepository<Account>
{
    Task<Account?> FindByEmail(string accountEmail);
}