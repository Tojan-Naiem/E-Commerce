using E_Commerce.DAL.Repository.Interfaces;
using E_Commerce.Data;
using E_Commerce.Model;
using Microsoft.EntityFrameworkCore;
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
        public async Task DecreaseProductQuantityAsync(List<(long productId,int quantity)> items)
        {
            var productIds = items.Select(i => i.productId).ToList();
            if (productIds is null) throw new Exception("Product not found");

            var products = await _dbContext.Products.Where(p => productIds.Contains(p.Id)).ToListAsync();
            if (products is null) throw new Exception("Product not found");

            foreach (var product in products)
            {
                var item = items.First(i => i.productId == product.Id);
                if (product.Quantity < item.quantity)
                {
                    throw new Exception("Product stock not enough");
                }
                product.Quantity -= item.quantity;
            }
           
            await _dbContext.SaveChangesAsync();

        }
    }
}
