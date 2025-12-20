using E_Commerce.DAL.Model;
using E_Commerce.DAL.Repository.Interfaces;
using E_Commerce.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.DAL.Repository.Classes
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public OrderRepository(
            ApplicationDbContext dbContext
            )
        {
            _dbContext = dbContext;
        }
        public async Task<Order?> GetUserByOrderId (int orderId){
            return await _dbContext.Orders.Include(o => o.User)
                 .FirstOrDefaultAsync(o => o.Id == orderId);
        }
        public async Task AddOrderAsync(Order order)
        {
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();
        }
        public async Task AddOrderItemsAsync(List<OrderItem> orderItems)
        {
           await _dbContext.OrderItems.AddRangeAsync(orderItems);
        }
        public async Task<List<Order>> GetByStatusAsync(OrderStatus status)
        {
            return await _dbContext.Orders.Where(o => o.Status == status).OrderByDescending(O=>O.OrderDate).ToListAsync();
        }
        public async Task<List<Order>> GetAllWithUserAsync(string UserId)
        {
            return await _dbContext.Orders.Where(o => o.UserId == UserId).OrderByDescending(O => O.OrderDate).ToListAsync();
        }
        public async Task<bool>ChangeStatusAsync(int orderId,OrderStatus newStatus)
        {
            var order = await _dbContext.Orders.FindAsync(orderId);
            if (order is null)
                throw new Exception("there's no order with this id");
            order.Status = newStatus;
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }
    }
}
