using E_Commerce.Model.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.DAL.Repository
{
    public interface ICategoryRepository
    {
        public Task<List<Category>> GetAllCategories(string lang = "en");
        public Category GetCategoryById(long id);
        public void SaveCategory(Category category);
        public  void SaveChangesInDatabase();
        public  void RemoveCategory(Category category);
        public Category GetCategoryByIdWithDetails(long id);
    }
}
