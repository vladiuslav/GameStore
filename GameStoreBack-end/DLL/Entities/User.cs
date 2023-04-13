using GameStore.DataLogic.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Entities
{
    public class User : BaseEntity
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        public string? AvatarImageUrl { get; set; }
        [Required]
        public int PasswordSaltId { get; set; }
        [Required]
        public PasswordWithSalt PasswordWithSalt { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Cart> CartItems{ get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
