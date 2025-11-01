using E_Commerce.BLL.Service;
using E_Commerce.DAL.Repository;
using E_Commerce.Data;
using E_Commerce.DTO.Request;
using E_Commerce.DTO.Response;
using E_Commerce.Model;
using E_Commerce.Model.Category;
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
            var categories = await _categoryRepository.GetAllCategories(lang);
            var categoryDTOs = categories.Select(c => new CategoryResponseDTO
            {
                Id = c.Id,
                CategoryTranslationResponses = c.categoryTranslations
                .Where(t => t.Language == lang)
                .Select(t => new CategoryTranslationResponse
                {
                    Name = t.Name,
                    Language = t.Language
                }).ToList()
            }).ToList();
            return categoryDTOs;

        }
        public CategoryResponseDTO GetCategory(long id)
        {
            var category = _categoryRepository.GetCategoryByIdWithDetails(id);
            if (category is null) return null;
            return category.Adapt<CategoryResponseDTO>();
        }
        public void Create(CategoryRequestDTO categoryDTO)
        {

            Category category = new Category()
            {
                Status = categoryDTO.Status,
                categoryTranslations = categoryDTO.categoryTranslations.Select(
                   t => new CategoryTranslation()
                   {
                       Name = t.Name,
                       Language = t.Language
                   }
                    ).ToList()
            };
            _categoryRepository.SaveCategory(category);
        }
        public async Task<bool> Update( long id, CategoryRequestDTO categoryRequestDTO)
        {
            var category = _categoryRepository.GetCategoryByIdWithDetails(id);
            if (category is null) return false;
            category.Status = categoryRequestDTO.Status;

            foreach (var categoryRequest in categoryRequestDTO.categoryTranslations)
            {

                var existingTranslation = category.categoryTranslations.FirstOrDefault(c => c.Language == categoryRequest.Language);
                Console.WriteLine(existingTranslation);
                if (existingTranslation is not null)
                {
                    existingTranslation.Name = categoryRequest.Name;

                }
                else
                {
                    category.categoryTranslations.Add(
                        new CategoryTranslation
                        {
                            Name = categoryRequest.Name,
                            Language = categoryRequest.Language,
                            CategoryId = category.Id
                        }
                        );


                }
            }
            _categoryRepository.SaveChangesInDatabase();

            return true;
        }

        public async Task<bool> ToggleStatus(long id)
        {
            var category = _categoryRepository.GetCategoryById(id);
            if (category is null) return false;
            category.Status = (category.Status == Status.Active) ? Status.In_active : Status.Active;
            _categoryRepository.SaveChangesInDatabase();
            return true;
        }
        public async Task<bool> Delete(long id)
        {
            var category = _categoryRepository.GetCategoryById(id);
            if (category is null) return false;
            _categoryRepository.RemoveCategory(category);
            return true;
        }
    }
}
