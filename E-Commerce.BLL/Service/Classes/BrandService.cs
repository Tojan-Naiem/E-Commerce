using E_Commerce.BLL.Service.Interfaces;
using E_Commerce.DAL.DTO.Request;
using E_Commerce.DAL.DTO.Response;
using E_Commerce.DAL.Model;
using E_Commerce.DAL.Repository;
using E_Commerce.DAL.Repository.Classes;
using E_Commerce.DAL.Repository.Interfaces;
using E_Commerce.DTO.Request;
using E_Commerce.DTO.Response;
using E_Commerce.Model;
using E_Commerce.Model;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.BLL.Service.Classes
{

    public class BrandService:GenericService<BrandRequestDTO,BrandResponseDTO,Brand> ,IBrandService
    {
        public BrandService(
IBrandRepository brandRepository      ):base(brandRepository)
        {
        }

      
    }
}
