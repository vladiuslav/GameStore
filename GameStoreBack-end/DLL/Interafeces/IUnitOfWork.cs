﻿using GameStore.DataLogic.Interafeces;
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
        ICommentRepository CommentRepository { get; }
        IOrderRepository OrderRepository { get; }
        ICartRepository CartRepository { get; }
        Task SaveAsync();
    }
}
