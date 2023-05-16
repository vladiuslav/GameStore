using System.ComponentModel.DataAnnotations;

namespace GameStore.WebAPI.Models.UserModels
{
    public class UserFullViewModel
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
        public string? AvatarImageUrl { get; set; }
        public ICollection<int> CommentsIds { get; set; }
    }
}
