using E_Commerce.Model;
using E_Commerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.DAL.Repository.Interfaces
{
    public interface IGenericRepository<T> where T:BaseModel
    {
        public Task<List<T>> GetAll(string lang = "en");
        public T? GetById(long id);
        public Task Save(T entity);
        public Task Remove(T entity);
        public Task SaveChangesInDatabase();
    }
}
