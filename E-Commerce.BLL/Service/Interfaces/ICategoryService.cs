using E_Commerce.BLL.Service.Interfaces;
using E_Commerce.DAL.Model;
using E_Commerce.DTO.Request;
using E_Commerce.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.BLL.Service
{
    public interface ICategoryService:IGenericService<CategoryRequestDTO,CategoryResponseDTO,Category>
    {
    



    }
}
