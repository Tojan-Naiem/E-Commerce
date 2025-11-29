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
    }
}
