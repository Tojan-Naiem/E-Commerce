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
        private readonly IFileService _fileService;
        private readonly IBrandRepository _brandRepository;
        public BrandService(
            IBrandRepository brandRepository,
            IFileService fileService
            ):base(brandRepository)
        {
            _brandRepository = brandRepository;
            _fileService = fileService;

        }
        
      
        public async Task<long> CreateFile(BrandRequestDTO request)
        {
            var entity = request.Adapt<Brand>();
            entity.CreatedAt = DateTime.Now;
            if (request.Image is not null)
            {
                var imagePath = await _fileService.UploadAsync(request.Image);
                entity.Image = imagePath;
            }
            await _brandRepository.Save(entity);
            return entity.Id;
        }
        public async Task<bool> DeleteFile(long id)
        {
            var entity = _brandRepository.GetById(id);
            bool successDelete = await _fileService.DeleteAsync(entity.Image);
            if (successDelete is false)
                throw new Exception("Error");
            await _brandRepository.Remove(entity);
            return true;
        }


    }
}
