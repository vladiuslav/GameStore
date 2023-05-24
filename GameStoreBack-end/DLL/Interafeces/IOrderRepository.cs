using DLL.Interafeces;
using GameStore.DataLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DataLogic.Interafeces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>> GetAllWithDetailsAsync();
        Task<Order> GetByIdWithDetailsAsync(int id);
    }
}
