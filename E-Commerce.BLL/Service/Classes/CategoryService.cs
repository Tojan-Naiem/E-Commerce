using E_Commerce.BLL.Service;
using E_Commerce.DAL.Model;
using E_Commerce.DAL.Repository;
using E_Commerce.Data;
using E_Commerce.DTO.Request;
using E_Commerce.DTO.Response;
using E_Commerce.Model;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.BLL.Repository
{
    public class CategoryService: ICategoryService
    {

        private readonly CategoryRepository _categoryRepository;
        public CategoryService(
             CategoryRepository categoryRepository
       )
        {
            _categoryRepository = categoryRepository;
        }
        
        public async Task<List<CategoryResponseDTO>> GetAll(string lang="en")
        {
            var categories = await _categoryRepository.GetAll(lang);
            var categoryDTOs = categories.Select(c => new CategoryResponseDTO
            {
                Id = c.Id,
                Name = c.Name,
              
            }).ToList();
            return categoryDTOs;

        }
        public CategoryResponseDTO GetCategory(long id)
        {
            var category = _categoryRepository.GetById(id);
            if (category is null) return null;
            return category.Adapt<CategoryResponseDTO>();
        }
        public async Task Create(CategoryRequestDTO categoryDTO)
        {

            Category category = new Category()
            {
                Status = categoryDTO.Status,
                   Name = categoryDTO.Name,
                     
            };
            await _categoryRepository.Save(category);
        }
        public async Task<bool> Update( long id, CategoryRequestDTO categoryRequestDTO)
        {
            var category = _categoryRepository.GetById(id);
            if (category is null) return false;
            category.Status = categoryRequestDTO.Status;
            category.Name = categoryRequestDTO.Name;

         
            await _categoryRepository.SaveChangesInDatabase();

            return true;
        }

        public async Task<bool> ToggleStatus(long id)
        {
            var category = _categoryRepository.GetById(id);
            if (category is null) return false;
            category.Status = (category.Status == Status.Active) ? Status.In_active : Status.Active;
            _categoryRepository.SaveChangesInDatabase();
            return true;
        }
        public async Task<bool> Delete(long id)
        {
            var category = _categoryRepository.GetById(id);
            if (category is null) return false;
            _categoryRepository.Remove(category);
            return true;
        }
    }
}
