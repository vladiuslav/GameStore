using DLL.Data;
using DLL.Entities;
using DLL.Interafeces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repositories
{
    public class PasswordWithSaltRepository : IPassswordWithSaltRepository
    {
        private readonly GameStoreDbContext _dbContext;
        public PasswordWithSaltRepository(GameStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(PasswordWithSalt entity)
        {
            await _dbContext.PasswordWithSalts.AddAsync(entity);
        }

        public void Delete(PasswordWithSalt entity)
        {
            _dbContext.PasswordWithSalts.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var password = await _dbContext.PasswordWithSalts.FirstAsync(g => g.Id == id);
            _dbContext.PasswordWithSalts.Remove(password);
        }

        public async Task<IEnumerable<PasswordWithSalt>> GetAllAsync()
        {
            return await _dbContext.PasswordWithSalts.ToListAsync();
        }

        public async Task<IEnumerable<PasswordWithSalt>> GetAllWithDetailsAsync()
        {
            return await _dbContext.PasswordWithSalts.Include(ps => ps.User).ToListAsync();
        }

        public async Task<PasswordWithSalt> GetByIdAsync(int id)
        {
            return await _dbContext.PasswordWithSalts.FirstAsync(ps => ps.Id == id);
        }

        public async Task<PasswordWithSalt> GetByIdWithDetailsAsync(int id)
        {
            return await _dbContext.PasswordWithSalts.Include(ps => ps.User).FirstAsync(ps => ps.Id == id);
        }

        public async Task UpdateAsync(PasswordWithSalt entity)
        {
            var passwordWithSalt = await _dbContext.PasswordWithSalts.FindAsync(entity.Id);
            passwordWithSalt.User = entity.User;
            passwordWithSalt.Password=entity.Password;
        }
    }
}
