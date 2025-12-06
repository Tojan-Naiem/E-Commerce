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
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.BLL.Service.Classes
{
    public class ProductService : GenericService<ProductRequest, ProductResponse, Product>, IProductService
    {
        private readonly IFileService _fileService;
        private readonly IProductRepository _productRepository;
        public ProductService(
            IProductRepository productRepository ,
            IFileService fileService
            ) : base(productRepository) {
            _productRepository = productRepository;
            _fileService = fileService;
        }
        public async Task<long> CreateFile(ProductRequest request)
        {
            var entity = request.Adapt<Product>();
            entity.CreatedAt = DateTime.Now;
            if(request.MainImage is not null)
            {
                var imagePath=await _fileService.UploadAsync(request.MainImage);
                entity.MainImage = imagePath;
            }
            await _productRepository.Save(entity);
            return entity.Id;
        }
        public async Task<bool> DeleteFile(long id)
        {
            var entity = _productRepository.GetById(id);
            bool successDelete = await _fileService.DeleteAsync(entity.MainImage);
            if (successDelete is false)
                throw new Exception("Error");
            await _productRepository.Remove(entity);
            return true;
        }
        public async Task<bool> UpdateProductAsync(long id, ProductRequest request)
        {
            var entity =  _productRepository.GetById(id);
            if (entity is null) return false;
            entity.Name = request.Name!;
            entity.Price = (decimal)request.Price!;
            entity.Quantity = request.Quantity;
            if (request.MainImage is not null)
            {
                if (!string.IsNullOrEmpty(entity.MainImage))
                {
                    await _fileService.DeleteAsync(entity.MainImage);
                }
                string newImage = await _fileService.UploadAsync(request.MainImage);
                entity.MainImage = newImage;

            }
            await _productRepository.SaveChangesInDatabase();
            return true;
        }
    }
}
