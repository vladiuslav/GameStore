using DLL.Data;
using DLL.Entities;
using System.Diagnostics.SymbolStore;
using System.Security.Cryptography;
using System.Text;

namespace GameStore.DataLogic.Data
{
    public class SeedData
    {
        public static readonly object locker = new object();
        public static void CreateData(UnitOfWork unitOfWork)
        {
            lock (locker)
            {   
                if (!(unitOfWork.GameRepository.GetAllAsync().Result.Count() == 0 &&
                unitOfWork.GenreRepository.GetAllAsync().Result.Count() == 0 &&
                unitOfWork.UserRepository.GetAllAsync().Result.Count() == 0
                ))
                {
                    return;
                }
                unitOfWork.GenreRepository.AddAsync(new Genre { Name = "Strategy" });
                unitOfWork.GenreRepository.AddAsync(new Genre { Name = "Rpg" });
                unitOfWork.GenreRepository.AddAsync(new Genre { Name = "Sports" });
                unitOfWork.GenreRepository.AddAsync(new Genre { Name = "Races" });
                unitOfWork.GenreRepository.AddAsync(new Genre { Name = "Action" });
                unitOfWork.GenreRepository.AddAsync(new Genre { Name = "Adventure" });
                unitOfWork.GenreRepository.AddAsync(new Genre { Name = "Puzzle & Skill" });
                unitOfWork.GenreRepository.AddAsync(new Genre { Name = "Other" });
                unitOfWork.GenreRepository.AddAsync(new Genre { Name = "Rpg2", ParentGenreId = 2 });
                unitOfWork.GenreRepository.AddAsync(new Genre { Name = "Races2", ParentGenreId = 4 });
                unitOfWork.Save();
                unitOfWork.GameRepository.AddAsync(
                   new Game
                   {
                       Name = "Game 1",
                       Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                       Price = 10.99M,
                       Genres = unitOfWork.GenreRepository.GetAllAsync().Result.Take(3).ToList(),
                   });
                unitOfWork.GameRepository.AddAsync(
                   new Game
                   {
                       Name = "Game 1",
                       Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                       Price = 12.99M,
                       Genres = unitOfWork.GenreRepository.GetAllAsync().Result.Take(1).ToList(),
                   });
                unitOfWork.GameRepository.AddAsync(
                   new Game
                   {
                       Name = "Game 2",
                       Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                       Price = 13M,
                       Genres = unitOfWork.GenreRepository.GetAllAsync().Result.Take(3).ToList(),
                   });
                unitOfWork.GameRepository.AddAsync(
                   new Game
                   {
                       Name = "Game 3",
                       Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                       Price = 19M,
                       Genres = unitOfWork.GenreRepository.GetAllAsync().Result.Take(4).ToList(),
                   });
                unitOfWork.GameRepository.AddAsync(
                   new Game
                   {
                       Name = "Game 4",
                       Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                       Price = 22.12M,
                       Genres = unitOfWork.GenreRepository.GetAllAsync().Result.Take(3).ToList(),
                   });
                unitOfWork.GameRepository.AddAsync(
                   new Game
                   {
                       Name = "Game 5",
                       Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                       Price = 4.99M,
                       Genres = unitOfWork.GenreRepository.GetAllAsync().Result.Take(2).ToList(),
                   });
                unitOfWork.Save();
                var passwordWithSalt = getPasswordWithSalt();
                unitOfWork.PassswordWithSaltRepository.AddAsync(passwordWithSalt);

                unitOfWork.UserRepository.AddAsync(
                   new User
                   {
                       FirstName = "Name1",
                       LastName = "LastName1",
                       UserName = "UserName1",
                       Email = "Email1@mail.com",
                       PasswordWithSaltId = 1,
                       PasswordWithSalt = passwordWithSalt
                   });
                unitOfWork.Save();
                
            }
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
