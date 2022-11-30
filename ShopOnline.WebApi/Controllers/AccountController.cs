using Microsoft.AspNetCore.Mvc;
using ShopOnline.Domain.Entities;
using ShopOnline.Domain.Exeptions;
using ShopOnline.Domain.IRepositories;
using ShopOnline.Domain.Services;

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

    [HttpPost("authorization")]
    public async Task<ActionResult<Account>> Authorization(Account account)
    {
        try
        {
            return account;
        }
        catch (Exception)
        {
            return Unauthorized();
        }
    }

    [HttpGet("get_account")]
    public async Task<ActionResult<Account>> GetAccount(Guid id)
    {
        try
        {
            var account = await _accountRepository.GetById(id);
            return Ok(account);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    [HttpPost("delete_account")]
    public async Task<IActionResult> DeleteAccount(Guid id)
    {
        await _accountRepository.DeleteById(id);
        return Ok();
    }
}