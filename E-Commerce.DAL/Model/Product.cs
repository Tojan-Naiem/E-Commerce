using E_Commerce.DAL.Model;

namespace E_Commerce.Model
{
    public class Product:BaseModel
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public decimal? Discount { get; set; }
        public int Quantity { get; set; }

        //img
        public string? MainImage { get; set; }

        public double? Rate { get; set; }
        public long CategoryId { get; set; }
        public Category Category { get; set; }
        public long? BrandId { get; set; }
        public Brand? Brand { get; set; }
    }
}
