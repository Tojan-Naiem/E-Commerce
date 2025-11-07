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
    public  interface IGenericService<TRequest,TResponse,TEntity>
    {
        public Task<List<TResponse>> GetAll(string lang = "en");
        public TResponse GetById(long id);
        public Task Create(TRequest entity);
        public Task<bool> Update(long id, TRequest entity);
        public Task<bool> ToggleStatus(long id);
        public Task<bool> Delete(long id);
    }
}
