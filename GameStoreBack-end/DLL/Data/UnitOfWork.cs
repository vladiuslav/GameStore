using DLL.Interafeces;
using Microsoft.EntityFrameworkCore;

namespace DLL.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GameStoreDbContext _context;
        public IGameRepository GameRepository { get; }
        public IGenreRepository GenreRepository { get; }
        public IUserRepository UserRepository { get; }
        public UnitOfWork(IGameRepository gameRepository, IGenreRepository genreRepository, IUserRepository userRepository, GameStoreDbContext context)
        {
            _context = context;
            GameRepository = gameRepository;
            GenreRepository = genreRepository;
            UserRepository = userRepository;
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
