using E_Commerce.DAL.DTO.Request;
using E_Commerce.DAL.DTO.Response;
using E_Commerce.DTO.Request;
using E_Commerce.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.BLL.Service.Interfaces
{
    public interface IBrandService
    {
        public Task<List<BrandResponseDTO>> GetAll(string lang = "en");
        public BrandResponseDTO GetById(long id);
        public Task Create(BrandRequestDTO brandRequest);
        public Task<bool> Update(long id, BrandRequestDTO brandRequest);
        public Task<bool> ToggleStatus(long id);
        public Task<bool> Delete(long id);

    }
}
