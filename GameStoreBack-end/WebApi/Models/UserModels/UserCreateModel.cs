using System.ComponentModel.DataAnnotations;

namespace GameStore.WebAPI.Models.UserModels
{
    public class UserCreateModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string FirstName { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string LastName { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string UserName { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(80)]
        public string Email { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
    }
}
