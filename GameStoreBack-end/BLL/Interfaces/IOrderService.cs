using BLL.Interfaces;
using GameStore.DataLogic.Entities;
using GameStrore.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStrore.BusinessLogic.Interfaces
{
    public interface IOrderService : ICrud<OrderModel>
    {
    }
}
