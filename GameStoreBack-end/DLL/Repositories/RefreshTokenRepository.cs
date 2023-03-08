using DLL.Data;
using DLL.Entities;
using DLL.Interafeces;
using Microsoft.EntityFrameworkCore;

namespace DLL.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly GameStoreDbContext _dbContext;

        public RefreshTokenRepository(GameStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(RefreshToken entity)
        {
            await _dbContext.RefreshTokens.AddAsync(entity);
        }

        public void Delete(RefreshToken entity)
        {
            _dbContext.RefreshTokens.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var refreshToken = await _dbContext.RefreshTokens.FirstAsync(rt => rt.Id == id);
            _dbContext.RefreshTokens.Remove(refreshToken);
        }

        public async Task<IEnumerable<RefreshToken>> GetAllAsync()
        {
            return await _dbContext.RefreshTokens.ToListAsync();
        }

        public async Task<RefreshToken> GetByIdAsync(int id)
        {
            return await _dbContext.RefreshTokens.FirstAsync(rt => rt.Id == id);
        }

        public async Task UpdateAsync(RefreshToken entity)
        {
            var refreshToken = await _dbContext.RefreshTokens.FindAsync(entity.Id);
            refreshToken.Token = entity.Token;
            refreshToken.User = entity.User;
            refreshToken.UserId = entity.UserId;
            refreshToken.IsRevoked = entity.IsRevoked;
            refreshToken.JwtId = entity.JwtId;
            refreshToken.DateAdded = entity.DateAdded;
            refreshToken.DateExpire = entity.DateExpire;
        }
    }
}
