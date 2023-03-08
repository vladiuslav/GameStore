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
        public IRefreshTokenRepository RefreshTokenRepository { get; }
        public UnitOfWork(IGameRepository gameRepository, IGenreRepository genreRepository, IUserRepository userRepository ,IRefreshTokenRepository refreshTokenRepository, GameStoreDbContext context)
        {
            _context = context;
            GameRepository = gameRepository;
            GenreRepository = genreRepository;
            UserRepository = userRepository;
            RefreshTokenRepository = refreshTokenRepository;
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
