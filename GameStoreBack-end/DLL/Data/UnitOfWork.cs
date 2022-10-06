using DLL.Interafeces;
using DLL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DLL.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private GameStoreDbContext db;

        public UnitOfWork(DbContextOptions<GameStoreDbContext> options)
        {
            this.db = new GameStoreDbContext(options);
        }

        IGameRepository gameRepository;
        public IGameRepository GameRepository
        {
            get
            {
                if(gameRepository == null)
                {
                    gameRepository = new GameRepository(db);
                }
                return gameRepository;
            }
        }
        IGanreRepository ganreRepository;
        public IGanreRepository GanreRepository
        {
            get
            {
                if (ganreRepository == null)
                {
                    ganreRepository = new GanreRepository(db);
                }
                return ganreRepository;
            }
        }
        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }
    }
}
