using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BeautyQueenApi.Constants
{
    public class AuthOptions
    {
        public const string KEY = "L2AaFLc28x28j2zqbz2Eq2cU";
        public const int ACCESS_TOKEN_LIFETIME = 1;
        public const int REFRESH_TOKEN_LIFETIME = 3;

        public const bool ValidateAudience = false;
        public const bool ValidateIssuer = false;
        public const bool ValidateIssuerSigningKey = true;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
