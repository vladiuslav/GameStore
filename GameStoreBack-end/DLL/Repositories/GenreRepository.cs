using DLL.Interafeces;
using DLL.Data;
using DLL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DLL.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly GameStoreDbContext _dbContext;
        public GenreRepository(GameStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(Genre entity)
        {
            await _dbContext.Genres.AddAsync(entity);
        }

        public void Delete(Genre entity)
        {
            _dbContext.Genres.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var genre = await _dbContext.Genres.FirstAsync(g => g.Id == id);
            _dbContext.Genres.Remove(genre);
        }

        public async Task<IEnumerable<Genre>> GetAllAsync()
        {
            return await _dbContext.Genres.ToListAsync();
        }

        public async Task<IEnumerable<Genre>> GetAllWithDetailsAsync()
        {
            return await _dbContext.Genres.Include(g=>g.Games).ToListAsync();
        }

        public async Task<Genre> GetByIdAsync(int id)
        {
            return await _dbContext.Genres.FirstAsync(g => g.Id == id);
        }

        public async Task<Genre> GetByIdWithDetailsAsync(int id)
        {
            return await _dbContext.Genres.Include(g=>g.Games).FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task UpdateAsync(Genre entity)
        {
            var genre = await _dbContext.Genres.FindAsync(entity.Id);
            genre.Name = entity.Name;
            genre.Games = entity.Games;
        }
    }
}
