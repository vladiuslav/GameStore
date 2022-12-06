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
        IGenreRepository genreRepository;
        public IGenreRepository GenreRepository
        {
            get
            {
                if (genreRepository == null)
                {
                    genreRepository = new GenreRepository(db);
                }
                return genreRepository;
            }
        }
        IUserRepository userRepository;
        public IUserRepository UserRepository
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new UserRepository(db);
                }
                return userRepository;
            }
        }
        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }
    }
}
