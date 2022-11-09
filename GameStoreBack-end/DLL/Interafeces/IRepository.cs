using DLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Interafeces
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> GetByIdAsync(int id);

        Task AddAsync(TEntity entity);

        void Delete(TEntity entity);

        Task DeleteByIdAsync(int id);

        Task UpdateAsync(TEntity entity);
    }
}
