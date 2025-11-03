using E_Commerce.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Model;
using E_Commerce.DAL.Model;

namespace E_Commerce.DAL.Utils
{
    public class SeedData : ISeedData
    {
        private readonly ApplicationDbContext _dbContext;
        public SeedData(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void DataSeeding()
        {
            if (_dbContext.Database.GetPendingMigrations().Any())
            {
                _dbContext.Database.Migrate();
            }
            if (!_dbContext.Categories.Any())
            {
                _dbContext.Categories.AddRange(
                    new Category { Name = "Clothes" },
                    new Category { Name = "Phones" }
                    );
            }
            if (!_dbContext.Brands.Any())
            {
                _dbContext.Brands.AddRange(
                    new Brand { Name = "Iphone" },
                    new Brand { Name = "Samsong" }
                    );
            }
            _dbContext.SaveChanges();
        }
    }
}
