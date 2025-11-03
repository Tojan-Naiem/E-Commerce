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

    public class BrandService:IBrandService
    {
        private readonly BrandRepository _brandRepository;
        public BrandService(
BrandRepository brandRepository      )
        {
            _brandRepository = brandRepository;
        }

        public async Task<List<BrandResponseDTO>> GetAll(string lang = "en")
        {
            var brands = await _brandRepository.GetAll(lang);
            var brandResponseDTOs = brands.Select(c => new BrandResponseDTO
            {
                Id = c.Id,
                Name = c.Name,

            }).ToList();
            return brandResponseDTOs;

        }
        public BrandResponseDTO GetById(long id)
        {
            var brand = _brandRepository.GetById(id);
            if (brand is null) return null;
            return brand.Adapt<BrandResponseDTO>();
        }
        public async Task Create(BrandRequestDTO brandRequest)
        {

            Brand brand = new Brand()
            {
                Status = brandRequest.Status,
                Name = brandRequest.Name,

            };
            await _brandRepository.Save(brand);
        }
        public async Task<bool> Update(long id, BrandRequestDTO brandRequestDTO)
        {
            var brand = _brandRepository.GetById(id);
            if (brand is null) return false;
            brand.Status = brandRequestDTO.Status;
            brand.Name = brandRequestDTO.Name;


            await _brandRepository.SaveChangesInDatabase();

            return true;
        }

        public async Task<bool> ToggleStatus(long id)
        {
            var brand = _brandRepository.GetById(id);
            if (brand is null) return false;
            brand.Status = (brand.Status == Status.Active) ? Status.In_active : Status.Active;
            await _brandRepository.SaveChangesInDatabase();
            return true;
        }
        public async Task<bool> Delete(long id)
        {
            var brand = _brandRepository.GetById(id);
            if (brand is null) return false;
           await _brandRepository.Remove(brand);
            return true;
        }
    }
}
