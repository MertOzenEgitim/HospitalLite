public interface IAuthService
{
    Task<TokenDto> LoginAsync(LoginDto loginDto);
}