using GameStrore.BusinessLogic.Models;
using System.ComponentModel.DataAnnotations;

namespace GameStore.WebAPI.Models
{
    public class CreateOrderModel
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
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required]
        [Phone(ErrorMessage = "Invalid Phone")]
        public string Phone { get; set; }
        public string PaymentType { get; set; }
        [MaxLength(600,ErrorMessage ="Comment max length 600")]
        public string Comment { get; set; }
        public CartModel[] CartModelsIds { get; set; }
    }
}
