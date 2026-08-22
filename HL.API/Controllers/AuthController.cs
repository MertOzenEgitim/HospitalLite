using HL.Core.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HL.API.Controllers;

[Route("api/[controller]")]
public class AuthController : BaseController
{
    private readonly IAuthService _authService;
    private readonly IUserService _userService;

    public AuthController(IAuthService authService, IUserService userService, ITokenService tokenService)
    {                                                          // ⚠ tokenService atanmıyor (Bölüm 24)
        _authService = authService;
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(CreateUserDto createUserDto)
    {
        await _userService.CreateAsync(createUserDto);
        return CreateResponse(
            ApiResponse<string>.SuccessResponse(
                data: "Kullanıcı başarıyla oluşturuldu. Giriş yapabilirsiniz.",
                message: "Kayıt başarılı.",
                messageKey:"auth.register_success"));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        var result = await _authService.LoginAsync(loginDto);
        return CreateResponse(
            ApiResponse<TokenDto>.SuccessResponse(result, "Giriş başarılı.","auth.login_success"));
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(RefreshTokenDto refreshTokenDto)
    {
        var result = await _authService.RefreshTokenLoginAsync(refreshTokenDto.RefreshToken);
        return CreateResponse(
            ApiResponse<TokenDto>.SuccessResponse(result, "Token yenilendi.","auth.token_refreshed"));
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdClaim))
            return CreateResponse(
                ApiResponse<string>.FailResponse("Kullanıcı kimliği token'da bulunamadı.","auth.missing_identity", 401));

        await _authService.LogoutAsync(int.Parse(userIdClaim));
        return CreateResponse(
            ApiResponse<string>.SuccessResponse(
                data: "Çıkış yapıldı. Refresh token iptal edildi.",
                message: "Çıkış başarılı.",
                messageKey:"auth.logout_success"));
    }
}