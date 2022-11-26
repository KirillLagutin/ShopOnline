using ShopOnline.Models;

namespace ShopOnline.Domain.Services;

public interface IAccountService
{
    Task<Account> Register(Account account);
}