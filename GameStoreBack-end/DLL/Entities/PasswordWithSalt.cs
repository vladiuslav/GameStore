using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Entities
{
    public class PasswordWithSalt : BaseEntity
    {
        [Required]
        public string Salt { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int UserId  { get; set; }
        [Required]
        public User User { get; set; }
    }
}
