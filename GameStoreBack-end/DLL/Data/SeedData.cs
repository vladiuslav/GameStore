using DLL.Data;
using DLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DataLogic.Data
{
    public class SeedData
    {
        public static async Task CreateDataAsync(UnitOfWork unitOfWork)
        {
            await unitOfWork.GenreRepository.AddAsync(new Genre { Name = "Strategy" });
            await unitOfWork.GenreRepository.AddAsync(new Genre { Name = "Rpg" });
            await unitOfWork.GenreRepository.AddAsync(new Genre { Name = "Sports" });
            await unitOfWork.GenreRepository.AddAsync(new Genre { Name = "Races" });
            await unitOfWork.GenreRepository.AddAsync(new Genre { Name = "Action" });
            await unitOfWork.GenreRepository.AddAsync(new Genre { Name = "Adventure" });
            await unitOfWork.GenreRepository.AddAsync(new Genre { Name = "Puzzle & Skill" });
            await unitOfWork.GenreRepository.AddAsync(new Genre { Name = "Other" });
            await unitOfWork.GenreRepository.AddAsync(new Genre { Name = "Rpg2", ParentGenreId = 2 });
            await unitOfWork.GenreRepository.AddAsync(new Genre { Name = "Races2", ParentGenreId = 4 });
            await unitOfWork.SaveAsync();
            await unitOfWork.GameRepository.AddAsync(
                new Game { Name = "Game 1",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    Price = 10.99M,
                    Genres=unitOfWork.GenreRepository.GetAllAsync().Result.Take(3).ToList(),
                });
            await unitOfWork.GameRepository.AddAsync(
                new Game
                {
                    Name = "Game 1",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    Price = 12.99M,
                    Genres = unitOfWork.GenreRepository.GetAllAsync().Result.Take(1).ToList(),
                });
            await unitOfWork.GameRepository.AddAsync(
                new Game
                {
                    Name = "Game 2",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    Price = 13M,
                    Genres = unitOfWork.GenreRepository.GetAllAsync().Result.Take(3).ToList(),
                });
            await unitOfWork.GameRepository.AddAsync(
                new Game
                {
                    Name = "Game 3",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    Price = 19M,
                    Genres = unitOfWork.GenreRepository.GetAllAsync().Result.Take(4).ToList(),
                });
            await unitOfWork.GameRepository.AddAsync(
                new Game
                {
                    Name = "Game 4",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    Price = 22.12M,
                    Genres = unitOfWork.GenreRepository.GetAllAsync().Result.Take(3).ToList(),
                });
            await unitOfWork.GameRepository.AddAsync(
                new Game
                {
                    Name = "Game 5",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    Price = 4.99M,
                    Genres = unitOfWork.GenreRepository.GetAllAsync().Result.Take(2).ToList(),
                });
            await unitOfWork.SaveAsync();

            var passwordWithSalt = getPasswordWithSalt();
            unitOfWork.PassswordWithSaltRepository.AddAsync(passwordWithSalt);
            await unitOfWork.UserRepository.AddAsync(
                new User
            {
                FirstName = "Name1",
                LastName = "LastName1",
                UserName = "UserName1",
                Email = "Email1@mail.com",
                PasswordSaltId = 1,
                PasswordWithSalt = passwordWithSalt

            });
            await unitOfWork.SaveAsync();

        }


        private static PasswordWithSalt getPasswordWithSalt()
        {
            var random = RandomNumberGenerator.Create();
            byte[] salt = new byte[10];
            random.GetBytes(salt);
            var saltString = Convert.ToBase64String(salt);

            var bytes = Encoding.UTF8.GetBytes("12345678" + saltString);
            var sha256 = SHA256.Create();
            var hash = sha256.ComputeHash(bytes);
            var hashString = Convert.ToBase64String(hash);

            return new PasswordWithSalt() { Salt = saltString, Password = hashString };
        }
    }
}
