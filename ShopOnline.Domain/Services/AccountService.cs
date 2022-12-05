using ShopOnline.Domain.IRepositories;
using Microsoft.AspNetCore.Identity;
using ShopOnline.Domain.Entities;
using ShopOnline.Domain.Exceptions;

namespace ShopOnline.Domain.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IPasswordHasher<Account> _hasher;
    private readonly ITokenService _tokenService;

    public AccountService(
        IAccountRepository accountRepository,
        IPasswordHasher<Account> hasher,
        ITokenService tokenService)
    {
        // Fail Fast
        _accountRepository = accountRepository 
                             ?? throw new ArgumentNullException(nameof(accountRepository));
        _hasher            = hasher 
                             ?? throw new ArgumentNullException(nameof(hasher));
        _tokenService      = tokenService 
                             ?? throw new ArgumentNullException(nameof(tokenService));
    }

    public async Task<Account> Register(Account account)
    {
        var passwordHash = _hasher.HashPassword(new Account(), account.Password);
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
    
    public async Task<(Account account, string token)> LogIn(string email, string password)
    {
        if (email == null) throw new ArgumentNullException(nameof(email));
        if (password == null) throw new ArgumentNullException(nameof(password));

        Account? account = await _accountRepository.FindByEmail(email);
        if (account is null)
        {
            throw new EmailUnregisteredExceptions();
        }

        var result = _hasher.VerifyHashedPassword(account,
            account.Password, password);
        if (result is PasswordVerificationResult.Failed)
        {
            throw new IncorrectPasswordException();
        }

        string token = _tokenService.GenerateToken(account);
        // реализовать _tokenService

        return (account, token);
    }
}