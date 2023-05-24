using DLL.Data;
using GameStore.DataLogic.Entities;
using GameStore.DataLogic.Interafeces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DataLogic.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly GameStoreDbContext _dbContext;
        public CartRepository(GameStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(Cart entity)
        {
            await _dbContext.Carts.AddAsync(entity);
        }

        public void Delete(Cart entity)
        {
            _dbContext.Carts.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var cart = await _dbContext.Carts.FirstAsync(c => c.Id == id);
            _dbContext.Carts.Remove(cart);
        }

        public async Task<IEnumerable<Cart>> GetAllAsync()
        {
            return await _dbContext.Carts.ToListAsync();
        }

        public async Task<IEnumerable<Cart>> GetAllWithDetailsAsync()
        {
            return await _dbContext.Carts
                .Include(c=>c.Game)
                .ToListAsync();
        }

        public async Task<Cart> GetByIdAsync(int id)
        {
            return await _dbContext.Carts.FirstAsync(c => c.Id == id);
        }

        public async Task<Cart> GetByIdWithDetailsAsync(int id)
        {
            var order = await _dbContext.Carts
                .Include(c => c.Game)
                .FirstOrDefaultAsync(c => c.Id == id);
            return order;
        }

        public async Task UpdateAsync(Cart entity)
        {
            var order = await _dbContext.Carts.FindAsync(entity.Id);
            order.Quantity = entity.Quantity;
        }
    }
}
