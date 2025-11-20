using E_Commerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.DAL.Model
{
    public class Cart
    {
        public long ProductId { get; set; }
        public Product Product { get; set; }
        public long UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int Count { get; set; }
    }
}
