using E_Commerce.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.DAL.Repository.Interfaces
{
    public interface IOrderRepository
    {
        public Task<Order> GetUserByOrderId(int orderId);
    }
}
