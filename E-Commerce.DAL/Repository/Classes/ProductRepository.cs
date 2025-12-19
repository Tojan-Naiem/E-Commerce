using E_Commerce.DAL.Repository.Interfaces;
using E_Commerce.Data;
using E_Commerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.DAL.Repository.Classes
{
    public class ProductRepository:GenericRepository<Product>,IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ProductRepository(
            ApplicationDbContext dbContext
       ) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task DecreaseProductQuantityAsync(long ProductId,int quantity)
        {
            var product =await _dbContext.Products.FindAsync(ProductId);
            if (product is null) throw new Exception("Product not found");
            if (product.Quantity < quantity)
            {
                throw new Exception("Product stock not enough");
            }
            product.Quantity -= quantity;
            await _dbContext.SaveChangesAsync();

        }
    }
}
