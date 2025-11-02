using E_Commerce.DTO.Request;
using E_Commerce.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.BLL.Service
{
    public interface ICategoryService
    {
        public  Task<List<CategoryResponseDTO>> GetAll(string lang = "en");
        public CategoryResponseDTO GetCategory(long id);
        public Task Create(CategoryRequestDTO categoryDTO);
        public Task<bool> Update(long id, CategoryRequestDTO categoryRequestDTO);
        public  Task<bool> ToggleStatus(long id);
        public  Task<bool> Delete(long id);



    }
}
