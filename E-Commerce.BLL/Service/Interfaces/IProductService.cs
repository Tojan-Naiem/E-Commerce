using E_Commerce.DAL.DTO.Request;
using E_Commerce.DAL.DTO.Response;
using E_Commerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.BLL.Service.Interfaces
{
    public interface IProductService:IGenericService<ProductRequest,ProductResponse,Product>
    {
        public Task<long> CreateFile(ProductRequest request);
        public Task<bool> DeleteFile(long id);
    }
}
