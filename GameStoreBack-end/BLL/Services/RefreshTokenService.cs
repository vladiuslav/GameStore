using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DLL.Entities;
using DLL.Interafeces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IUnitOfWork _unitOfWork;
        public RefreshTokenService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(RefreshToken model)
        {
            await _unitOfWork.RefreshTokenRepository.AddAsync(model);
            await _unitOfWork.SaveAsync();
        }

        public async Task<RefreshToken> GetTokenByToken(string token)
        {
            var tokens = await _unitOfWork.RefreshTokenRepository.GetAllAsync();
            return tokens.FirstOrDefault(x => x.Token == token);
        }
    }
}
