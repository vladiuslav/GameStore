using BLL.Interfaces;
using GameStrore.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStrore.BusinessLogic.Interfaces
{
    public interface IСartService : ICrud<CartModel>
    {
        Task<IEnumerable<CartModel>> GetUserCart(int userId);
    }
}
