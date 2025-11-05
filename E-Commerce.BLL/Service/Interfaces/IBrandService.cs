using E_Commerce.DAL.DTO.Request;
using E_Commerce.DAL.DTO.Response;
using E_Commerce.DAL.Model;
using E_Commerce.DTO.Request;
using E_Commerce.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.BLL.Service.Interfaces
{
    public interface IBrandService:IGenericService<BrandRequestDTO,BrandResponseDTO,Brand>
    {
     

    }
}
