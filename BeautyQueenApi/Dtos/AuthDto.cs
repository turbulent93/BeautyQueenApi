using BeautyQueenApi.Models;

namespace BeautyQueenApi.Dtos
{
    public class AuthDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public int UserId { get; set; }
        public string Login { get; set; }
        public string Role { get; set; }
    }
}
