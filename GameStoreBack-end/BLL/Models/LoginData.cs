using System.ComponentModel.DataAnnotations;

namespace BLL.Models
{
    public class LoginData
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
