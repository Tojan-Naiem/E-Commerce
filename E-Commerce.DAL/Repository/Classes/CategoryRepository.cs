using E_Commerce.DAL.Model;
using E_Commerce.DAL.Repository.Classes;
using E_Commerce.Data;
using E_Commerce.DTO.Request;
using E_Commerce.DTO.Response;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.DAL.Repository
{
    public class CategoryRepository : GenericRepository<Category>,ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public CategoryRepository(
            ApplicationDbContext dbContext
       ):base(dbContext)
        {
            _dbContext = dbContext;
        }
      
      

    }
}
