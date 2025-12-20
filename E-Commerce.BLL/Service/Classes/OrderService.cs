using E_Commerce.BLL.Service.Interfaces;
using E_Commerce.DAL.Model;
using E_Commerce.DAL.Repository.Interfaces;
using E_Commerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.BLL.Service.Classes
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(
            IOrderRepository orderRepository
            )
        {
            _orderRepository = orderRepository;
        }
        public async Task AddOrderAsync(Order order)
        {
            await _orderRepository.AddOrderAsync(order);
        }

        public async Task<bool> ChangeStatusAsync(int orderId, OrderStatus newStatus)
        {
           var result= await _orderRepository.ChangeStatusAsync(orderId, newStatus);
            return result;
        }

        public async Task<List<Order>> GetAllWithUserAsync(string UserId)
        {
            return await _orderRepository.GetAllWithUserAsync(UserId);
        }

        public async Task<List<Order>> GetByStatusAsync(OrderStatus status)
        {
            return await _orderRepository.GetByStatusAsync(status);
        }

        public async Task<Order> GetUserByOrderId(int orderId)
        {
            return await _orderRepository.GetUserByOrderId(orderId);
        }
    }
}
