using E_Commerce.BLL.Service.Interfaces;
using E_Commerce.DAL.DTO.Request;
using E_Commerce.DAL.DTO.Response;
using E_Commerce.DAL.Model;
using E_Commerce.DAL.Repository;
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
    }
}
