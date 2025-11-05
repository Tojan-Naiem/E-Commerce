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
    public  interface IGenericService<T> where T : BaseModel
    {
        public Task<List<T>> GetAll(string lang = "en");
        public T GetById(long id);
        public Task Create(T entity);
        public Task<bool> Update(long id, T entity);
        public Task<bool> ToggleStatus(long id);
        public Task<bool> Delete(long id);
    }
}
