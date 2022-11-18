using ShopOnline.Models;

namespace ShopOnline.WebApi.Services;

public interface IAccountService
{
    Task<Account> Register(Account account);
}