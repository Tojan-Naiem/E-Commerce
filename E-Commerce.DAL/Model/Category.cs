using E_Commerce.Model;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.DAL.Model
{
    public class Category:BaseModel
    {
        public string? Name { get; set; }
        public List<Product> Products = new List<Product>();

    }
}
