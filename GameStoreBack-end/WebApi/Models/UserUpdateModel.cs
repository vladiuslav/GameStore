using System.ComponentModel.DataAnnotations;

namespace GameStore.WebAPI.Models
{
    public class UserUpdateModel
    {
        public int Id { get; set; }
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
        public string? Password { get; set; }
    }
}
