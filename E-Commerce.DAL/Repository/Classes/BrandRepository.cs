using E_Commerce.DAL.Model;
using E_Commerce.DAL.Repository.Interfaces;
using E_Commerce.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.DAL.Repository.Classes
{
    public class BrandRepository:GenericRepository<Brand>,IBrandRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public BrandRepository(
            ApplicationDbContext dbContext
       ) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
