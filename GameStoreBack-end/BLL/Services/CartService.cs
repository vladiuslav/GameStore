using AutoMapper;
using DLL.Entities;
using DLL.Interafeces;
using GameStore.DataLogic.Entities;
using GameStrore.BusinessLogic.Interfaces;
using GameStrore.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStrore.BusinessLogic.Services
{
    public class CartService : IСartService
    {
        public IUnitOfWork _unitOfWork { get;}
        public IMapper _mapper { get; }

        public CartService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddAsync(CartModel model)
        {
            var cart = _mapper.Map<Cart>(model);
            await _unitOfWork.CartRepository.AddAsync(cart);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _unitOfWork.CartRepository.DeleteByIdAsync(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<CartModel>> GetAllAsync()
        {
            var carts = await _unitOfWork.CartRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CartModel>>(carts);
        }

        public async Task<CartModel> GetByIdAsync(int id)
        {
            var cart = await _unitOfWork.CartRepository.GetByIdAsync(id);
            return _mapper.Map<CartModel>(cart);
        }

        public async Task UpdateAsync(CartModel model)
        {
            var cart = _mapper.Map<Cart>(model);
            await _unitOfWork.CartRepository.UpdateAsync(cart);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<CartModel>> GetUserCart(int userId)
        {
            var carts = await _unitOfWork.CartRepository.GetAllWithDetailsAsync();
            return _mapper.Map<IEnumerable<CartModel>>(carts.Where(c=>c.UserId== userId).ToList());
        }
    }
}
