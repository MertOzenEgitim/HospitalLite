using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HL.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IUserService _userService;

    public AuthController(IAuthService authService, IUserService userService, ITokenService tokenService)
    {
        _authService = authService;
        _userService = userService;
    }

    // Yeni Kullanıcı Kaydı
    [HttpPost("register")]
    public async Task<IActionResult> Register(CreateUserDto createUserDto)
    {
        await _userService.CreateAsync(createUserDto);
        return Ok("Kullanıcı başarıyla oluşturuldu. Giriş yapabilirsiniz.");
    }

    // Giriş ve Token Alma
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        var result = await _authService.LoginAsync(loginDto);
        return Ok(result); // Geriye TokenDto (Access + Refresh) döner
    }

    // Refresh Token ile yeni set almak için kullanılır
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(RefreshTokenDto refreshTokenDto)
    {
        var result = await _authService.RefreshTokenLoginAsync(refreshTokenDto.RefreshToken);
        return Ok(result); // Yeni TokenDto (yeni Access + yeni Refresh)
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        var userIdClaim=User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if(string.IsNullOrEmpty(userIdClaim))
            return Unauthorized("Kullanıcı kimliği token'da bulunamadı");

        await _authService.LogoutAsync(int.Parse(userIdClaim));
        return Ok("Çıkış yapıldı. Refresh token iptal edildi.");
    }
}