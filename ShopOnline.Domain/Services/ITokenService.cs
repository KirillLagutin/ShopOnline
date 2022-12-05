using ShopOnline.Domain.Entities;

namespace ShopOnline.Domain.Services;

public interface ITokenService
{
    string GenerateToken(Account account);
}