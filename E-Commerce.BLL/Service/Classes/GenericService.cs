using Azure;
using Azure.Core;
using E_Commerce.BLL.Service.Interfaces;
using E_Commerce.DAL.Model;
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
    public class GenericService<TRequest, TResponse, TEntity> : IGenericService<TRequest, TResponse, TEntity> where TEntity:BaseModel  where TResponse:class
    {
        private readonly IGenericRepository<TEntity> _genericRepository;
        public GenericService(
            IGenericRepository<TEntity> genericRepository)
        {
            _genericRepository = genericRepository;
             
        }
        public async Task Create(TRequest request)
        {
            TEntity entity = request.Adapt<TEntity>();
            await _genericRepository.Save(entity);
        }

        public async Task<bool> Delete(long id)
        {
            var entity = _genericRepository.GetById(id);
            if (entity is null) return false;
           await _genericRepository.Remove(entity);
            return true;
        }

        public async Task<List<TResponse>> GetAll(string lang = "en")
        {
            var entities =await  _genericRepository.GetAll(lang);
          
            return entities.Adapt<List<TResponse>>();
        }

        public TResponse? GetById(long id)
        {
            var entity = _genericRepository.GetById(id);
            return entity is null ? null : entity.Adapt<TResponse>();
        }

        public async Task<bool> ToggleStatus(long id)
        {
            var entity = _genericRepository.GetById(id);
            if (entity is null) return false;
            entity.Status = (entity.Status == Status.Active) ? Status.In_active : Status.Active;
            await _genericRepository.SaveChangesInDatabase();
            return true;
        }

        public async Task<bool> Update(long id, TRequest request)
        {
            var entity = _genericRepository.GetById(id);
            if (entity is null) return false;
            var updatedEntity = request.Adapt(entity);
            await _genericRepository.SaveChangesInDatabase();

            return true;
        }
    }
}
