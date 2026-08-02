public interface IAuthService
{
    Task<TokenDto> LoginAsync(LoginDto loginDto);
    Task<TokenDto> RefreshTokenLoginAsync(string RefreshToken);
    Task LogoutAsync(int userId);
}