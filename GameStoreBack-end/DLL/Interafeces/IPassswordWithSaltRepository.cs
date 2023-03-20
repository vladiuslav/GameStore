using DLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Interafeces
{
    public interface IPassswordWithSaltRepository : IRepository<PasswordWithSalt>
    {
        Task<IEnumerable<PasswordWithSalt>> GetAllWithDetailsAsync();
        Task<PasswordWithSalt> GetByIdWithDetailsAsync(int id);
    }
}
