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
    public class OrderRepository : IOrderRepository
    {
        private readonly GameStoreDbContext _dbContext;
        public OrderRepository(GameStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(Order entity)
        {
            await _dbContext.Orders.AddAsync(entity);
        }

        public void Delete(Order entity)
        {
            _dbContext.Orders.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var order = await _dbContext.Orders.FirstAsync(o => o.Id == id);
            _dbContext.Orders.Remove(order);
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _dbContext.Orders.ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetAllWithDetailsAsync()
        {
            return await _dbContext.Orders
                .Include(o=>o.CartItems)
                .ToListAsync();
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await _dbContext.Orders.FirstAsync(o => o.Id == id);
        }

        public async Task<Order> GetByIdWithDetailsAsync(int id)
        {
            var order = await _dbContext.Orders
                .Include(o => o.CartItems)
                .FirstOrDefaultAsync(o => o.Id == id);
            return order;
        }

        public async Task UpdateAsync(Order entity)
        {
            var order = await _dbContext.Orders.FindAsync(entity.Id);
            order.Email = entity.Email;
            order.Phone= entity.Phone;
            order.FirstName = entity.FirstName;
            order.LastName = entity.LastName;
            order.CartItems = entity.CartItems;
            order.PaymentType = entity.PaymentType;
        }
    }
}
