using DLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Interafeces
{
    public interface IGanreRepository : IRepository<Ganre>
    {
        Task<IEnumerable<Ganre>> GetAllWithDetailsAsync();
        Task<Ganre> GetByIdWithDetailsAsync(int id);
    }
}
