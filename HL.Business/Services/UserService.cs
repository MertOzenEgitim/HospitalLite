using HL.Core.Entities;
using HL.Core.Interfaces;

public class UserService:IUserService
{
    private readonly IGenericRepository<AppUser> _userRepo;
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IGenericRepository<AppUser> userRepo, IUnitOfWork unitOfWork)
    {
        _userRepo = userRepo;
        _unitOfWork = unitOfWork;
    }

    public async Task CreateAsync(CreateUserDto dto)
    {
        AppUser user=new()
        {
            CreatedBy="admin",
            FirstName=dto.FirstName,
            LastName=dto.LastName,
            Email=dto.Email,
            PasswordHash=BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Role=dto.role
        };

        await _userRepo.AddAsync(user);
        await _unitOfWork.CommitAsync();
    }
    public async Task UpdateRefereshToken(int userId,string refreshToken,DateTime expiration)
    {
        var user=await _userRepo.GetByIdAsync(userId);
        if(user!=null)
        {
            user.RefreshToken=refreshToken;
            user.RefreshTokenEndDate=expiration;
            await _unitOfWork.CommitAsync();
        }
    }
}