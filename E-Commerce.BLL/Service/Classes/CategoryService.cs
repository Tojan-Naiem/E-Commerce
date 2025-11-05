using E_Commerce.BLL.Service;
using E_Commerce.BLL.Service.Classes;
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
    public class CategoryService: GenericService<CategoryRequestDTO,CategoryResponseDTO,Category>,ICategoryService
    {

        public CategoryService(
             ICategoryRepository categoryRepository
       ):base(categoryRepository)
        {
        }
      
    }
}
