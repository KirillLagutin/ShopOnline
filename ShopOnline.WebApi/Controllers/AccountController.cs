using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopOnline.Domain.Entities;
using ShopOnline.Domain.Exceptions;
using ShopOnline.Domain.IRepositories;
using ShopOnline.Domain.Services;
using ShopOnline.HttpModels.Requests;
using ShopOnline.HttpModels.Responses;

namespace ShopOnline.WebApi.Controllers;

[ApiController]
[Route("account")]
public class AccountController : ControllerBase
{
    private readonly IAccountRepository _accountRepository;
    private readonly IAccountService _accountService;

    public AccountController(IAccountRepository accountRepository,
        IAccountService accountService)
    {
        _accountRepository = accountRepository;
        _accountService = accountService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<Account>> Register(Account account)
    {
        try
        {
            var newAccount = await _accountService.Register(account);
            
            return newAccount;
        }
        catch (EmailAlreadyRegisteredExeption)
        {
            return BadRequest(new
            {
                Message = "Такой Email уже зарегистрирован"
            });
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> LogIn(LoginRequest request)
    {
        try
        {
            var (account, token)
                = await _accountService.LogIn(request.Email, request.Password);

            var response = new LoginResponse
            {
                Id = account.Id,
                Name = account.Name,
                Email = request.Email,
                Token = token
            };

            return response;
        }
        catch (EmailUnregisteredExceptions)
        {
            return BadRequest(new
            {
                Message = "Такой Email не зарегистрирован"
            });
        }
        catch (Exception)
        {
            return BadRequest(
            new
            {
                Message = "Пароль не совпадает"
            });
        }
    }

    [Authorize]
    [HttpGet("get_account")]
    public async Task<ActionResult<AccountInfoResponse>> GetCurrentAccount()
    {
        var strId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var guid = Guid.Parse(strId);
        var account = await _accountRepository.GetById(guid);

        return new AccountInfoResponse(guid, account.Name, account.Email);
    }

    [HttpPost("delete_account")]
    public async Task<IActionResult> DeleteAccount(Guid id)
    {
        try
        {
            await _accountRepository.DeleteById(id);
            return Ok();
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }
}