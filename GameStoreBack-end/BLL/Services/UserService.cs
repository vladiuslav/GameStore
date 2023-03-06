using BLL.Interfaces;
using BLL.Models;
using DLL.Data;
using DLL.Interafeces;
using DLL.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

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
            await _unitOfWork.UserRepository.AddAsync(_mapper.Map<User>(model));
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _unitOfWork.UserRepository.DeleteByIdAsync(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<UserModel>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<UserModel>>(await _unitOfWork.UserRepository.GetAllAsync());
        }

        public async Task<UserModel> GetByIdAsync(int id)
        {
            return _mapper.Map<UserModel>(await _unitOfWork.UserRepository.GetByIdAsync(id));
        }
        public async Task<UserModel> GetUserByEmailAsync(string email)
        {
            return _mapper.Map<UserModel>(
                    (await _unitOfWork.UserRepository.GetAllAsync())
                    .FirstOrDefault(u=>u.Email==email)
                );
        }
        public async Task<UserModel> GetUserByUserNameAsync(string name)
        {
            return _mapper.Map<UserModel>(
                    (await _unitOfWork.UserRepository.GetAllAsync())
                    .FirstOrDefault(u => u.UserName == name)
                );
        }

        public async Task UpdateAsync(UserModel model)
        {
            await _unitOfWork.UserRepository.UpdateAsync(_mapper.Map<User>(model));
            await _unitOfWork.SaveAsync();
        }

    }
}
