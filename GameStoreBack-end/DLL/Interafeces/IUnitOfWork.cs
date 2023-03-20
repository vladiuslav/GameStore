using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Interafeces
{
    public interface IUnitOfWork
    {
        IGameRepository GameRepository { get; }
        IGenreRepository GenreRepository { get; }
        IUserRepository UserRepository { get; }
        IPassswordWithSaltRepository PassswordWithSaltRepository { get; }
        IRefreshTokenRepository RefreshTokenRepository { get; }
        Task SaveAsync();
    }
}
