using DLL.Interafeces;
using GameStore.DataLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DataLogic.Interafeces
{
    public interface ICartRepository : IRepository<Cart>
    {
        Task<IEnumerable<Cart>> GetAllWithDetailsAsync();
        Task<Cart> GetByIdWithDetailsAsync(int id);
    }
}
