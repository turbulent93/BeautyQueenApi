using BeautyQueenApi.Dtos;
using BeautyQueenApi.Models;
using System.Security.Claims;

namespace BeautyQueenApi.Services
{
    public interface IAdminService
    {
        Task Register(RegisterDto adminDto);
        Task<AuthDto> Login(RegisterDto adminDto);
        Task<TokenDto> RefreshToken(AuthDto loginDto);
        Task<IEnumerable<User>> Get(string? registered);
    }
}
