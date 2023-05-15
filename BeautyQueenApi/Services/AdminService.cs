using AutoMapper;
using BeautyQueenApi.Constants;
using BeautyQueenApi.Data;
using BeautyQueenApi.Dtos;
using BeautyQueenApi.Models;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace BeautyQueenApi.Services
{
    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AdminService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool UserExists(string login)
        {
            return (_context.User?.Any(e => e.Role.Name == "Admin" && e.Login == login)).GetValueOrDefault();
        }

        public string CreateAccessToken(User admin)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Name, admin.Login)
            };

            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                notBefore: now,
                claims: claims,
                expires: now.AddDays(1),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public async Task<string> CreateRefreshToken(User admin)
        {
            var refreshToken = new byte[256];
            var rng = RandomNumberGenerator.Create();

            rng.GetBytes(refreshToken);

            admin.RefreshToken = Convert.ToBase64String(refreshToken);
            admin.ExpiresIn = DateTime.UtcNow.AddDays(AuthOptions.REFRESH_TOKEN_LIFETIME);

            await _context.SaveChangesAsync();

            return admin.RefreshToken;
        }

        public async Task<User?> Find(string login)
        {
            return await _context.User.FirstOrDefaultAsync(e => e.Login == login);
        }

        public ClaimsPrincipal? GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = AuthOptions.ValidateAudience,
                ValidateIssuer = AuthOptions.ValidateIssuer,
                ValidateIssuerSigningKey = AuthOptions.ValidateIssuerSigningKey,
                IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey()
            };

            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);

            if (validatedToken == null)
            {
                return null;
            }

            return principal;
        }

        public async Task<AuthDto> Login(RegisterDto adminDto)
        {
            User? admin = await Find(adminDto.Login);

            if(admin == null)
            {
                throw new Exception("Admin is not found");
            }

            if (!BCrypt.Net.BCrypt.Verify(adminDto.Password, admin.Password))
            {
                throw new Exception("Invalid login or password");
            }

            return new AuthDto
            {
                AccessToken = CreateAccessToken(admin),
                RefreshToken = await CreateRefreshToken(admin)
            };
        }

        public async Task Register(RegisterDto adminDto)
        {
            if (UserExists(adminDto.Login))
            {
                throw new Exception("Admin is already exists");
            }

            User admin = _mapper.Map<User>(adminDto);

            _context.User.Add(admin);

            await _context.SaveChangesAsync();
        }
        
        public async Task<TokenDto> RefreshToken(AuthDto loginDto)
        {
            ClaimsPrincipal? principal = GetPrincipalFromToken(loginDto.AccessToken);

            if (principal?.Identity?.Name == null)
            {
                throw new Exception("Invalid token");
            }

            User? admin = await Find(principal.Identity.Name);
            
            if (admin == null)
            {
                throw new Exception("Admin is not found");
            }

            if(admin.RefreshToken != loginDto.RefreshToken)
            {
                throw new Exception("Invalid refresh token" + admin.RefreshToken);
            }

            return new TokenDto
            {
                AccessToken = CreateAccessToken(admin)
            };
        }

        public async Task<IEnumerable<User>> Get(string? registered)
        {
            IEnumerable<User> users = await _context.User.ToListAsync();

            if(registered != null)
                return users.Where(x => _context.Employee.FirstOrDefault(e => e.UserId == x.Id) == null);

            return users;
        }
    }
}
