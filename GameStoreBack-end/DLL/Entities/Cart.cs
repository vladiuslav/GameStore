using DLL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DataLogic.Entities
{
    public class Cart : BaseEntity
    {
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int GameId { get; set; }
        public Game Game { get; set; }
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
