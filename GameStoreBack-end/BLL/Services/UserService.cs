using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DLL.Entities;
using DLL.Interafeces;
using System.Security.Cryptography;
using System.Text;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task AddAsync(UserModel model)
        {

            var user = _mapper.Map<User>(model);
            var salt = GenerateSalt(10);

            var passwordWithSalt = new PasswordWithSalt { Salt = salt, Password = HashPassword(model.Password, salt), User = user };
            await _unitOfWork.PassswordWithSaltRepository.AddAsync(passwordWithSalt);

            user.PasswordWithSalt = passwordWithSalt;
            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var user = await _unitOfWork.UserRepository.GetByIdWithDetailsAsync(id);
            await _unitOfWork.UserRepository.DeleteByIdAsync(id);
            _unitOfWork.PassswordWithSaltRepository.Delete(user.PasswordWithSalt);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<UserModel>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<UserModel>>(await _unitOfWork.UserRepository.GetAllWithDetailsAsync());
        }

        public async Task<UserModel> GetByIdAsync(int id)
        {
            return _mapper.Map<UserModel>(await _unitOfWork.UserRepository.GetByIdWithDetailsAsync(id));
        }
        public async Task<UserModel> GetUserByEmailAsync(string email)
        {
            return _mapper.Map<UserModel>(
                    (await _unitOfWork.UserRepository.GetAllWithDetailsAsync())
                    .FirstOrDefault(u => u.Email == email)
                );
        }
        public async Task<UserModel> GetUserByUserNameAsync(string name)
        {
            return _mapper.Map<UserModel>(
                    (await _unitOfWork.UserRepository.GetAllWithDetailsAsync())
                    .FirstOrDefault(u => u.UserName == name)
                );
        }

        public async Task UpdateAsync(UserModel model)
        {
            var user = _mapper.Map<User>(model);

            var salt = GenerateSalt(10);
            var passwordWithSalt = (await _unitOfWork.PassswordWithSaltRepository.GetAllWithDetailsAsync()).First(ps => ps.UserId == user.Id);

            passwordWithSalt.Salt = salt;
            passwordWithSalt.Password = HashPassword(model.Password, salt);

            await _unitOfWork.PassswordWithSaltRepository.UpdateAsync(passwordWithSalt);
            await _unitOfWork.UserRepository.UpdateAsync(user);
            await _unitOfWork.SaveAsync();
        }

        public async Task<UserModel> GetUserByLoginData(LoginData loginData)
        {

            var user = (await _unitOfWork.UserRepository.GetAllWithDetailsAsync()).FirstOrDefault(u => u.Email == loginData.Email);
            if (user == null)
            {
                return null;
            }

            var password = HashPassword(loginData.Password, user.PasswordWithSalt.Salt);

            if (password != user.PasswordWithSalt.Password)
            {
                return null;
            }
            var userModel = _mapper.Map<UserModel>(user);
            return userModel;
        }

        private string GenerateSalt(int length)
        {
            var random = RandomNumberGenerator.Create();
            byte[] salt = new byte[length];
            random.GetBytes(salt);
            return Convert.ToBase64String(salt);
        }

        public static string HashPassword(string password, string salt)
        {
            var bytes = Encoding.UTF8.GetBytes(password + salt);
            var sha256 = SHA256.Create();
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }


    }
}
