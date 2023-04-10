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
    public class CommentRepository : ICommentRepository
    {
        private readonly GameStoreDbContext _dbContext;
        public CommentRepository(GameStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(Comment entity)
        {
            await _dbContext.Comments.AddAsync(entity);
        }

        public void Delete(Comment entity)
        {
            _dbContext.Comments.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var comment = await _dbContext.Comments.FirstAsync(c => c.Id == id);
            _dbContext.Comments.Remove(comment);
        }

        public async Task<IEnumerable<Comment>> GetAllAsync()
        {
            return await _dbContext.Comments.ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetAllWithDetailsAsync()
        {
            return await _dbContext.Comments
                .Include(c=>c.User)
                .Include(c=>c.Game)
                .Include(c => c.RepliedComment)
                .ToListAsync();
        }

        public async Task<Comment> GetByIdAsync(int id)
        {
            return await _dbContext.Comments.FirstAsync(g => g.Id == id);
        }

        public async Task<Comment> GetByIdWithDetailsAsync(int id)
        {
            var comment = await _dbContext.Comments
                .Include(c => c.User)
                .Include(c => c.Game)
                .Include(c => c.RepliedComment)
                .FirstOrDefaultAsync(g => g.Id == id);
            return comment;
        }
         
        public async Task UpdateAsync(Comment entity)
        {
            var comment = await _dbContext.Comments.FindAsync(entity.Id);
            comment.Text = entity.Text;
            comment.RepliedComment = entity.RepliedComment;
        }
    }
}
