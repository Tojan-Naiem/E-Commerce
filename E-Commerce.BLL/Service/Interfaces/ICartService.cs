using E_Commerce.DAL.DTO.Request;
using E_Commerce.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.BLL.Service.Interfaces
{
    public interface ICartService
    {
        public bool addToCart(CartRequest Cart, string UserId);
    }
}
