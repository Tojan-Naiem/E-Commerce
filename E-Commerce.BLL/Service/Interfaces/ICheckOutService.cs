using E_Commerce.DAL.DTO.Request;
using E_Commerce.DAL.DTO.Response;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.BLL.Service.Interfaces
{
    public interface ICheckOutService
    {
            public Task<CheckOutResponse> ProcessPaymentAsync(CheckOutRequest request, string UserId, HttpRequest httpRequest);
            public Task<bool> HandlePaymentSuccessAsync(int orderId);
    }
}
