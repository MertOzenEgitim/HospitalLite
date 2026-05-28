public interface IUserService
{
    Task CreateAsync(CreateUserDto dto);
    Task UpdateRefereshToken(int userId,string refreshToken,DateTime expiration);
}

