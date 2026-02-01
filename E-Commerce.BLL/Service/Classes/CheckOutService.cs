using E_Commerce.BLL.Service.Interfaces;
using E_Commerce.DAL.DTO.Request;
using E_Commerce.DAL.DTO.Response;
using E_Commerce.DAL.Model;
using E_Commerce.DAL.Repository.Classes;
using E_Commerce.DAL.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
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
        private readonly IOrderRepository _orderRepository;
        private readonly IEmailSender _emailSender;
        public CheckOutService(
            ICartRepository cartRepository,
            IOrderRepository orderRepository,
            IEmailSender emailSender
            )
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
            _emailSender = emailSender;
        }

        public async Task<bool> HandlePaymentSuccessAsync(int orderId)
        {
            var order = await _orderRepository.GetUserByOrderId(orderId);
            var subject = "";
            var body = "";
            if (order.PaymentMethod == PaymentMethod.Visa)
            {
                order.Status = OrderStatus.Approved;
                var carts = await _cartRepository.GetAsync(order.UserId);
                var orderItems = carts.Select(cartItem => new OrderItem
                {
                    OrderId = orderId,
                    ProductId = cartItem.ProductId,
                    TotalPrice = cartItem.Product.Price * cartItem.Count
                }).ToList();
                await _orderRepository.AddOrderItemsAsync(orderItems);
                await _cartRepository.ClearCartAsync(order.UserId);
                subject = "Payment Successful";
                body = $"Thank u for ur payment , ur payment for order {orderId}, total amount={order.TotalAmount}";
            }
            else if (order.PaymentMethod == PaymentMethod.Cash)
            {
                subject = "Order Successful";
                body = $"Thank u for ur order , ur order  {orderId}, total amount={order.TotalAmount}";
            }
            else return false;
                await _emailSender.SendEmailAsync(order.User.Email, subject, body);
            return true;


        }

        public async Task<CheckOutResponse> ProcessPaymentAsync(CheckOutRequest request, string UserId,HttpRequest httpRequest)
        {
            var cartItems = await _cartRepository.GetAsync(UserId);
            if(cartItems is null)
            {
                return new CheckOutResponse()
                {
                    Success = false,
                    Message="No Cart for this user"
                };
            }
            
            if (request.PaymentMethod == DAL.Model.PaymentMethod.Cash)
            {
                Order order = new Order()
                {
                    UserId = UserId,
                    PaymentMethod = PaymentMethod.Cash,
                    TotalAmount = cartItems.Sum(c => c.Product.Price * c.Count)
                };
                return new CheckOutResponse()
                {
                    Success = true,
                    Message = "Payment session created successfully",
                };

            }
            if (request.PaymentMethod == DAL.Model.PaymentMethod.Visa)
            {
                Order order = new Order()
                {
                    UserId = UserId,
                    PaymentMethod = PaymentMethod.Visa,
                    TotalAmount = cartItems.Sum(c => c.Product.Price * c.Count)

                };
                await _orderRepository.AddOrderAsync(order);
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    LineItems = new List<SessionLineItemOptions>
            {

            },
                    Mode = "payment",
                    SuccessUrl = $"{httpRequest.Scheme}://{httpRequest.Host}/api/checkout/success/{order.Id}",
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
                order.PaymentId = session.Id;
                return new CheckOutResponse()
                {
                    Success = true,
                    Message = "Payment session created successfully",
                    PaymentId = session.Id,
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
