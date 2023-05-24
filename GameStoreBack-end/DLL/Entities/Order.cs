using DLL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DataLogic.Entities
{
    public class Order : BaseEntity
    {
        [Required]
        public string FirstName { get; set; }
        [Required] 
        public string LastName { get; set; }
        [Required] 
        public string Email { get; set; }
        [Required] 
        public string Phone { get; set; }
        [Required]
        public string PaymentType { get; set; }
        public string Comment { get; set; }
        public ICollection<Cart> CartItems { get; set; }

    }
}
