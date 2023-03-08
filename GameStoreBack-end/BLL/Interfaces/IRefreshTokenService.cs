using BLL.Models;
using DLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IRefreshTokenService
    {
        Task AddAsync(RefreshToken model);
        Task<RefreshToken> GetTokenByToken(string token);
    }
}
