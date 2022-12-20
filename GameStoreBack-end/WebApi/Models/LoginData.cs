using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class LoginData
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public bool RememberMe { get; set; }
    }
}
