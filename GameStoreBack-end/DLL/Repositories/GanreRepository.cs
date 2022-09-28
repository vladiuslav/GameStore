using DLL.Interafeces;
using DLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repositories
{
    public class GanreRepository : IGanreRepository
    {
        public Task AddAsync(Ganre entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Ganre entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Ganre>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Ganre> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Ganre entity)
        {
            throw new NotImplementedException();
        }
    }
}
