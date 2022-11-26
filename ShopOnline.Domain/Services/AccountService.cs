using ShopOnline.Domain.Exeptions;
using ShopOnline.Domain.IRepositories;
using ShopOnline.Models;

namespace ShopOnline.Domain.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<Account> Register(Account account)
    {
        var existedAccount = await _accountRepository
            .FindByEmail(account.Email);

        var accountExists = existedAccount is not null;
        if (accountExists)
        {
            throw new EmailAlreadyRegisteredExeption(
                "Такой Email уже зарегистрирован", account.Email);
        }

        await _accountRepository.Add(account);
        return account;
    }

}