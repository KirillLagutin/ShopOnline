using Microsoft.AspNetCore.Mvc;
using ShopOnline.Models;
using ShopOnline.WebApi.Repositories.IRepositories;
using ShopOnline.WebApi.Services;

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
        catch (Exception e)
        {
            return BadRequest(new
            {
                Message = "Такой Email уже зарегистрирован"
            });
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