using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Entities
{
    public class RefreshToken : BaseEntity
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public string JwtId { get; set; }
        [Required]
        public bool IsRevoked { get; set; }
        [Required]
        public DateTime DateAdded { get; set; }
        [Required]
        public DateTime DateExpire { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public User User { get; set; }
    }
}
