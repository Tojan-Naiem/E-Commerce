using E_Commerce.BLL.Service.Interfaces;
using E_Commerce.DAL.DTO.Request;
using E_Commerce.DAL.Model;
using E_Commerce.DAL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.BLL.Service.Classes
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        public async Task<bool> addToCart(CartRequest Cart, string UserId)
        {
            var newItem = new Cart()
            {
                Count = 1,
                UserId=UserId,
                ProductId=Cart.ProductId
            };
            return await _cartRepository.Add(newItem);
        }
    }
}
