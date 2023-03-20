using DLL.Data;
using DLL.Entities;
using DLL.Interafeces;
using Microsoft.EntityFrameworkCore;

namespace DLL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly GameStoreDbContext _dbContext;
        public UserRepository(GameStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(User entity)
        {
            await _dbContext.Users.AddAsync(entity);
        }

        public void Delete(User entity)
        {
            _dbContext.Users.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var game = await _dbContext.Users.FirstAsync(g => g.Id == id);
            _dbContext.Users.Remove(game);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<IEnumerable<User>> GetAllWithDetailsAsync()
        {
            return await _dbContext.Users.Include(u=>u.PasswordWithSalt).ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _dbContext.Users.FirstAsync(g => g.Id == id);
        }

        public async Task<User> GetByIdWithDetailsAsync(int id)
        {
            return await _dbContext.Users.Include(u => u.PasswordWithSalt).FirstAsync(g => g.Id == id);
        }

        public async Task UpdateAsync(User entity)
        {
            var user = await _dbContext.Users.FindAsync(entity.Id);
            user.FirstName = entity.FirstName;
            user.LastName = entity.LastName;
            user.UserName = entity.UserName;
            user.Email = entity.Email;
            user.PasswordWithSalt = entity.PasswordWithSalt;
            user.AvatarImageUrl = entity.AvatarImageUrl;
        }

    }
}
