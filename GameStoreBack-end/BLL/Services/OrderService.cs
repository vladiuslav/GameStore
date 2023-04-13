using AutoMapper;
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
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task AddAsync(OrderModel model)
        {
            await _unitOfWork.OrderRepository.AddAsync(_mapper.Map<Order>(model));
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _unitOfWork.OrderRepository.DeleteByIdAsync(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<OrderModel>> GetAllAsync()
        {
            var orders = await _unitOfWork.OrderRepository.GetAllWithDetailsAsync();
            return _mapper.Map<IEnumerable<OrderModel>>(orders);
        }

        public async Task<OrderModel> GetByIdAsync(int id)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(id);
            return _mapper.Map<OrderModel>(order);
        }

        public async Task UpdateAsync(OrderModel model)
        {
            var order = _mapper.Map<Order>(model);
            await _unitOfWork.OrderRepository.UpdateAsync(order);
            await _unitOfWork.SaveAsync();
        }
    }
}
