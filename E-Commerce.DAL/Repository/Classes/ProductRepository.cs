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
    }
}
