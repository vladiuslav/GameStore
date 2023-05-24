using GameStrore.BusinessLogic.Models;

namespace GameStore.WebAPI.Models
{
    public class CreateOrderModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string PaymentType { get; set; }
        public string Comment { get; set; }
        public CartModel[] CartModelsIds { get; set; }
    }
}
