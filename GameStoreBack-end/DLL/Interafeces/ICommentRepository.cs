using DLL.Entities;
using DLL.Interafeces;
using GameStore.DataLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DataLogic.Interafeces
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Task<IEnumerable<Comment>> GetAllWithDetailsAsync();
        Task<Comment> GetByIdWithDetailsAsync(int id);
    }
}
