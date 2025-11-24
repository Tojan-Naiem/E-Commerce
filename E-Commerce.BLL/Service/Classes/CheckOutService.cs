using E_Commerce.BLL.Service.Interfaces;
using E_Commerce.DAL.DTO.Request;
using E_Commerce.DAL.DTO.Response;
using E_Commerce.DAL.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.BLL.Service.Classes
{
    public class CheckOutService : ICheckOutService
    {
        private readonly ICartRepository _cartRepository;
        public CheckOutService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public Task<bool> HandlePaymentSuccessAsync(int orderId)
        {
            throw new NotImplementedException();
        }

        public async Task<CheckOutResponse> ProcessPaymentAsync(CheckOutRequest request, string UserId,HttpRequest httpRequest)
        {
            var cartItems = await _cartRepository.Get(UserId);
            if(cartItems is null)
            {
                return new CheckOutResponse()
                {
                    Success = false,
                    Message="No Cart for this user"
                };
            }
            if (request.PaymentMethod == "Cash")
            {
                return new CheckOutResponse()
                {
                    Success = true,
                    Message = "Payment session created successfully",
                };
            }
            if (request.PaymentMethod == "Visa")
            {
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    LineItems = new List<SessionLineItemOptions>
            {

            },
                    Mode = "payment",
                    SuccessUrl = $"{httpRequest.Scheme}://{httpRequest.Host}/api/checkout/success",
                    CancelUrl = $"{httpRequest.Scheme}://{httpRequest.Host}/api/checkout/cancel",
                };
                foreach(var item in cartItems)
                {
                    options.LineItems.Add(
                        new SessionLineItemOptions
                        {
                            PriceData = new SessionLineItemPriceDataOptions
                            {
                                Currency = "USD",
                                ProductData = new SessionLineItemPriceDataProductDataOptions
                                {
                                    Name = item.Product.Name,
                                    Description = item.Product.Description,
                                },
                                UnitAmount =(long)item.Product.Price,
                            },
                            Quantity = item.Count,
                        }
                        );
                }
                var service = new SessionService();
                var session = service.Create(options);
                return new CheckOutResponse()
                {
                    Success = true,
                    Message = "Payment session created successfully",
                    Url = session.Url
                };
            }
            return new CheckOutResponse()
            {
                Success = false,
                Message = "Invalid",
            };
        }
    }
}
