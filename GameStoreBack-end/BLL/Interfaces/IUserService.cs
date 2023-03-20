using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUserService : ICrud<UserModel>
    {
        Task<UserModel> GetUserByEmailAsync(string email);
        Task<UserModel> GetUserByLoginData(LoginData loginData);
        Task<UserModel> GetUserByUserNameAsync(string userName);
    }
}
