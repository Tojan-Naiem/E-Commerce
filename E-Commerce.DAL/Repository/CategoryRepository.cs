using E_Commerce.Data;
using E_Commerce.DTO.Request;
using E_Commerce.DTO.Response;
using E_Commerce.Model.Category;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.DAL.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public CategoryRepository(
            ApplicationDbContext dbContext
       )
        {
            _dbContext = dbContext;
        }

        public async Task<List<Category>> GetAllCategories(string lang = "en")
        {
            var categories = _dbContext.Categories
                .Include(c => c.categoryTranslations)
                .OrderByDescending(c => c.CreatedAt)
                .ToList();
            return categories;
        }
        public Category GetCategoryById(long id)
        {
            var category = _dbContext.Categories.Find(id);
            return category;
        }
        public Category GetCategoryByIdWithDetails(long id)
        {
            var category = _dbContext.Categories.Include(c => c.categoryTranslations).FirstOrDefault(c => c.Id == id);
            return category;
        }
        public async void SaveCategory(Category category)
        {
            _dbContext.Categories.AddAsync(category);
            SaveChangesInDatabase();
        }
              public async void RemoveCategory(Category category)
        {
            _dbContext.Categories.Remove(category);
            SaveChangesInDatabase();

        
        }
        public async void SaveChangesInDatabase()
        {
            _dbContext.SaveChanges();

        }




    }
}
