using E_Commerce.BLL.Service.Interfaces;
using E_Commerce.DAL.DTO.Request;
using E_Commerce.DAL.DTO.Response;
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
        public async Task<bool> AddToCart(CartRequest Cart, string UserId)
        {
            var newItem = new Cart()
            {
                Count = 1,
                UserId=UserId,
                ProductId=Cart.ProductId
            };
            return await _cartRepository.Add(newItem);
        }

        public async Task<CartSummary> GetUserCart(string UserId)
        {
            var UserCart =await _cartRepository.Get(UserId);
            var response = new CartSummary()
            {
                Items = UserCart.Select(c => new CartResponse()
                {
                    ProductId = c.ProductId,
                    ProductName = c.Product.Name,
                    Count = c.Count,
                    Price=c.Product.Price
                }
                ).ToList()
            };
            return response;
        }
        public async Task<bool> DeleteCart(string UserId)
        {
            var UserCart = await _cartRepository.Get(UserId);
            if (UserCart is null)
                return false;
            foreach( var cart in UserCart)
            {
               await _cartRepository.Delete(cart);

            }
            await _cartRepository.SaveChangesInDatabase();

            return true;
        }
    }
}
