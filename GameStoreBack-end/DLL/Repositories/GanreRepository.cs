using DLL.Interafeces;
using DLL.Data;
using DLL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DLL.Repositories
{
    public class GanreRepository : IGanreRepository
    {
        private readonly GameStoreDbContext _dbContext;
        public GanreRepository(GameStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(Ganre entity)
        {
            await _dbContext.Ganres.AddAsync(entity);
        }

        public void Delete(Ganre entity)
        {
            _dbContext.Ganres.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var ganre = await _dbContext.Ganres.FirstAsync(g => g.Id == id);
            _dbContext.Ganres.Remove(ganre);
        }

        public async Task<IEnumerable<Ganre>> GetAllAsync()
        {
            return await _dbContext.Ganres.ToListAsync();
        }

        public async Task<IEnumerable<Ganre>> GetAllWithDetailsAsync()
        {
            return await _dbContext.Ganres.Include(g=>g.Games).ToListAsync();
        }

        public async Task<Ganre> GetByIdAsync(int id)
        {
            return await _dbContext.Ganres.FirstAsync(g => g.Id == id);
        }

        public async Task<Ganre> GetByIdWithDetailsAsync(int id)
        {
            return await _dbContext.Ganres.Include(g=>g.Games).FirstOrDefaultAsync(g => g.Id == id);
        }

        public void Update(Ganre entity)
        {
            _dbContext.Entry(entity).CurrentValues.SetValues(entity);
        }
    }
}
