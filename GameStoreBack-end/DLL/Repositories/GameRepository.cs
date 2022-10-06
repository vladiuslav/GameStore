﻿using DLL.Interafeces;
using DLL.Data;
using DLL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DLL.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly GameStoreDbContext _dbContext;
        public GameRepository(GameStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Game entity)
        {
            await _dbContext.Games.AddAsync(entity);
        }

        public void Delete(Game entity)
        {
            _dbContext.Games.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
           var game = await _dbContext.Games.FirstAsync(g => g.Id==id);
            _dbContext.Games.Remove(game);
        }

        public async Task<IEnumerable<Game>> GetAllAsync()
        {
            return await _dbContext.Games.ToListAsync();
        }

        public async Task<IEnumerable<Game>> GetAllWithDetailsAsync()
        {
            return await _dbContext.Games.Include(g=>g.Ganres).ToListAsync();
        }

        public async Task<Game> GetByIdAsync(int id)
        {
            return await _dbContext.Games.FirstAsync(g => g.Id==id);
        }

        public async Task<Game> GetByIdWithDetailsAsync(int id)
        {
            return await _dbContext.Games.Include(g=>g.Ganres).FirstOrDefaultAsync(g => g.Id == id);
        }

        public void Update(Game entity)
        {
            _dbContext.Entry(entity).CurrentValues.SetValues(entity);
        }
    }
}
