using DLL.Entities;
using DLL.Interafeces;
using GameStore.DataLogic.Interafeces;
using Microsoft.EntityFrameworkCore;
using System;
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

            if (context.Games.Count() == 0 &&
                context.Genres.Count() == 0 &&
                context.Users.Count() == 0)
            {
                seedDate(_context);
            }
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
        private void seedDate(GameStoreDbContext context)
        {
            context.Genres.AddRange(
                new Genre { Name = "Strategy", Games = new List<Game>() },
                new Genre { Name = "Rpg", Games = new List<Game>() },
                new Genre { Name = "Sports", Games = new List<Game>() },
                new Genre { Name = "Races", Games = new List<Game>() },
                new Genre { Name = "Action", Games = new List<Game>() },
                new Genre { Name = "Adventure", Games = new List<Game>() },
                new Genre { Name = "Puzzle & skill", Games = new List<Game>() },
                new Genre { Name = "Other", Games = new List<Game>() }
            );
            context.Games.AddRange(
                new Game
                {
                    Name = "Game name 1",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit." +
                    " Praesent vulputate, metus sed semper tincidunt, diam sem laoreet orci," +
                    " sed iaculis leo enim sed orci. Phasellus bibendum iaculis aliquet." +
                    " Suspendisse potenti. Aenean volutpat maximus mattis. Sed vel tellus nulla.",

                    Price = 10.59m,
                    Genres = new List<Genre>() 
                },
                new Game
                {
                    Name = "Game name 2",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit." +
                    " Praesent vulputate, metus sed semper tincidunt, diam sem laoreet orci," +
                    " sed iaculis leo enim sed orci. Phasellus bibendum iaculis aliquet." +
                    " Suspendisse potenti. Aenean volutpat maximus mattis. Sed vel tellus nulla.",

                    Price = 4.99m,
                    Genres = new List<Genre>()
                },
                new Game
                {   
                    Name = "Game name 3",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit." +
                    " Praesent vulputate, metus sed semper tincidunt, diam sem laoreet orci," +
                    " sed iaculis leo enim sed orci. Phasellus bibendum iaculis aliquet." +
                    " Suspendisse potenti. Aenean volutpat maximus mattis. Sed vel tellus nulla.",

                    Price = 14.30m,
                    Genres = new List<Genre>()
                },
                new Game
                {
                    Name = "Game name 4",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit." +
                    " Praesent vulputate, metus sed semper tincidunt, diam sem laoreet orci," +
                    " sed iaculis leo enim sed orci. Phasellus bibendum iaculis aliquet." +
                    " Suspendisse potenti. Aenean volutpat maximus mattis. Sed vel tellus nulla.",

                    Price = 9.99m,
                    Genres = new List<Genre>()
                },
                new Game
                {
                    Name = "Game name 5",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit." +
                    " Praesent vulputate, metus sed semper tincidunt, diam sem laoreet orci," +
                    " sed iaculis leo enim sed orci. Phasellus bibendum iaculis aliquet." +
                    " Suspendisse potenti. Aenean volutpat maximus mattis. Sed vel tellus nulla.",

                    Price = 12.00m,
                    Genres = new List<Genre>()
                },
                new Game
                {
                    Name = "Game name 6",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit." +
                    " Praesent vulputate, metus sed semper tincidunt, diam sem laoreet orci," +
                    " sed iaculis leo enim sed orci. Phasellus bibendum iaculis aliquet." +
                    " Suspendisse potenti. Aenean volutpat maximus mattis. Sed vel tellus nulla.",

                    Price = 10.10m,
                    Genres = new List<Genre>()
                });

            var passwordWithSalt = getPasswordWithSalt();
            context.PasswordWithSalts.Add(passwordWithSalt);
            context.Users.Add(new User
            {
                FirstName = "Name1",
                LastName = "LastName1",
                UserName = "UserName1",
                Email = "Email1@mail.com",
                PasswordSaltId = 1,
                PasswordWithSalt = passwordWithSalt

            });
            context.SaveChanges();
        }
        private PasswordWithSalt getPasswordWithSalt()
        {
            var random = RandomNumberGenerator.Create();
            byte[] salt = new byte[10];
            random.GetBytes(salt);
            var saltString = Convert.ToBase64String(salt);

            var bytes = Encoding.UTF8.GetBytes("12345678" + saltString);
            var sha256 = SHA256.Create();
            var hash = sha256.ComputeHash(bytes);
            var hashString = Convert.ToBase64String(hash);

            return new PasswordWithSalt() {Salt = saltString, Password = hashString };
        }
    }
}
