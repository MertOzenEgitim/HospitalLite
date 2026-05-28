using HL.Core.Entities;

public interface ITokenService
{
    TokenDto CreateToken(AppUser user);
}