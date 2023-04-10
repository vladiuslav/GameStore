using AutoMapper;
using BLL.Interfaces;
using DLL.Interafeces;
using GameStore.DataLogic.Entities;
using GameStore.DataLogic.Interafeces;
using GameStrore.BusinessLogic.Interfaces;
using GameStrore.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStrore.BusinessLogic.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CommentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddAsync(CommentModel model)
        {
            var comment = _mapper.Map<Comment>(model);
            await _unitOfWork.CommentRepository.AddAsync(comment);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _unitOfWork.CommentRepository.DeleteByIdAsync(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<CommentModel>> GetAllAsync()
        {
            var comments = _mapper.Map<IEnumerable<CommentModel>>(await _unitOfWork.CommentRepository.GetAllWithDetailsAsync());
            return comments;
        }

        public async Task<CommentModel> GetByIdAsync(int id)
        {
            var comment = _mapper.Map<CommentModel>(await _unitOfWork.CommentRepository.GetByIdAsync(id));
            return comment;
        }

        public async Task<IEnumerable<CommentModel>> GetCommentsByGameIdAsync(int id)
        {
            var allComments = await _unitOfWork.CommentRepository.GetAllWithDetailsAsync();
            var comments = _mapper.Map<IEnumerable<CommentModel>>(allComments);
            var FilteredComents = comments.Where(c=>c.GameId == id).ToList();
            return FilteredComents;
        }

        public async Task UpdateAsync(CommentModel model)
        {
            var coment = _mapper.Map<Comment>(model);
            await _unitOfWork.CommentRepository.UpdateAsync(coment);
            await _unitOfWork.SaveAsync();
        }
    }
}
