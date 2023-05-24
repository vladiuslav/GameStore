using GameStore.DataLogic.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStrore.BusinessLogic.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string PaymentType { get; set; }
        public string Comment { get; set; }
        public ICollection<int> CartItemIds { get; set; }
    }
}
