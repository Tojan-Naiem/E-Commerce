using E_Commerce.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.DAL.DTO.Request
{
    public class CheckOutRequest
    {
        public PaymentMethod PaymentMethod { get; set; }
    }
}
