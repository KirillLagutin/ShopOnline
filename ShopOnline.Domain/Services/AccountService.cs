using System.Security.Authentication;
using ShopOnline.Domain.Exeptions;
using ShopOnline.Domain.IRepositories;
using ShopOnline.Models;
using Microsoft.AspNetCore.Identity;
using ShopOnline.Models.Dto;

namespace ShopOnline.Domain.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IPasswordHasher<AccountDto> _hasher;

    public AccountService(IAccountRepository accountRepository,
        IPasswordHasher<AccountDto> hasher)
    {
        _accountRepository = accountRepository;
        _hasher = hasher;
    }

    public async Task<Account> Register(Account account)
    {
        var passwordHash = _hasher.HashPassword(new AccountDto(), account.Password);
        account.Password = passwordHash;
        
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
    
    public async Task<Account> Authorization(Account account)
    {
        var passwordHash = _hasher.HashPassword(new AccountDto(), account.Password);

        PasswordVerificationResult result = _hasher.VerifyHashedPassword(
            new AccountDto(), passwordHash, account.Password);
        if (result != PasswordVerificationResult.Failed)
        {
            return account;
        }

        return null;
    }

}