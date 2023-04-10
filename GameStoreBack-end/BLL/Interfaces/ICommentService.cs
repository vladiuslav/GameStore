using BLL.Interfaces;
using GameStrore.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStrore.BusinessLogic.Interfaces
{
    public interface ICommentService : ICrud<CommentModel>
    {
        Task<IEnumerable<CommentModel>> GetCommentsByGameIdAsync(int id);
    }
}
