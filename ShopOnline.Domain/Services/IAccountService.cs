using ShopOnline.Domain.Entities;

namespace ShopOnline.Domain.Services;

public interface IAccountService
{
    Task<Account> Register(Account account);
    Task<Account> Authorization(Account account);
}