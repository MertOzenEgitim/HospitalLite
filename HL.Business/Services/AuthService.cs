using HL.Core.Entities;
using HL.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

public class AuthService:IAuthService
{
    private readonly IGenericRepository<AppUser> _userRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;
    private readonly IUserService _userService;

    public AuthService(IGenericRepository<AppUser> userRepo, IUnitOfWork unitOfWork, ITokenService tokenService, IUserService userService)
    {
        _userRepo = userRepo;
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
        _userService = userService;
    }

    public async Task<TokenDto> LoginAsync(LoginDto loginDto)
    {
        var user=await _userRepo.Where(x=>x.Email==loginDto.Email)
            .Include(x=>x.UserClaims)
            .FirstOrDefaultAsync();

        if(user==null || !BCrypt.Net.BCrypt.Verify(loginDto.Password,user.PasswordHash))
            throw new UnauthorizedException("Geçersiz e-eposta veya şifre.","auth.invalid_credentials");

        var tokenDto=_tokenService.CreateToken(user);

        user.RefreshToken=tokenDto.RefreshToken;
        user.RefreshTokenEndDate=DateTime.Now.AddDays(7);
        await _unitOfWork.CommitAsync();

        return tokenDto;
    }

    public async Task LogoutAsync(int userId)
    {
        await _userService.RevokeRefreshToken(userId);
    }

    public async Task<TokenDto> RefreshTokenLoginAsync(string refreshToken)
    {
        var user=await _userRepo.Where(x=>x.RefreshToken==refreshToken)
        .Include(x=>x.UserClaims)
        .FirstOrDefaultAsync();

        if(user==null)
            throw new UnauthorizedException("Geçersiz refresh token.","auth.invalid_refresh_token");

        if(user.RefreshTokenEndDate==null || user.RefreshTokenEndDate<DateTime.Now)
            throw new UnauthorizedException("Refresh token süresi dolmuş. Lütfen tekrar giriş yapınız.","auth.refresh_token_expired");

        var tokenDto=_tokenService.CreateToken(user);

        user.RefreshToken=tokenDto.RefreshToken;
        user.RefreshTokenEndDate=DateTime.Now.AddDays(7);
        await _unitOfWork.CommitAsync();

        return tokenDto;
    }
}
