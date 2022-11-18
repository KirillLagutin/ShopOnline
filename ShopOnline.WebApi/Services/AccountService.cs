using Microsoft.AspNetCore.Mvc;
using ShopOnline.Models;
using ShopOnline.Models.Exeptions;
using ShopOnline.WebApi.Repositories.IRepositories;

namespace ShopOnline.WebApi.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    [HttpPost("register")]
    public async Task<Account> Register(Account account)
    {
        var existedAccount = await _accountRepository
            .FindByEmail(account.Email);

        var accountExist = existedAccount is not null;
        if (accountExist)
        {
            throw new EmailAlreadyRegisteredExeption(
                "Такой Email уже зарегистрирован", account.Email);
        }
        
        await _accountRepository.Add(account);
        return account;
    }

}