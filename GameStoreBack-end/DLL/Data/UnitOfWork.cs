using DLL.Entities;
using DLL.Interafeces;
using GameStore.DataLogic.Data;
using GameStore.DataLogic.Interafeces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace DLL.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GameStoreDbContext _context;
        public IGameRepository GameRepository { get; }
        public IGenreRepository GenreRepository { get; }
        public IUserRepository UserRepository { get; }
        public IRefreshTokenRepository RefreshTokenRepository { get; }
        public IPassswordWithSaltRepository PassswordWithSaltRepository { get; }
        public ICommentRepository CommentRepository { get; }
        public ICartRepository CartRepository { get; }
        public IOrderRepository OrderRepository { get; }
        public UnitOfWork(IGameRepository gameRepository, 
            IGenreRepository genreRepository,
            IUserRepository userRepository ,
            IRefreshTokenRepository refreshTokenRepository,
            IPassswordWithSaltRepository passswordWithSaltRepository, 
            ICommentRepository commentRepository,
            ICartRepository cartRepository,
            IOrderRepository orderRepository,
            GameStoreDbContext context
            )
        {
            _context = context;
            GameRepository = gameRepository;
            GenreRepository = genreRepository;
            UserRepository = userRepository;
            RefreshTokenRepository = refreshTokenRepository;
            PassswordWithSaltRepository = passswordWithSaltRepository;
            CommentRepository = commentRepository;
            CartRepository = cartRepository;
            OrderRepository = orderRepository;

            SeedData.CreateData(this);            
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
