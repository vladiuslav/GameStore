using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class AuthResultViewModel
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public string RefreshToken { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime RefreshTokenExpiresAt { get; set; }
    }
}
