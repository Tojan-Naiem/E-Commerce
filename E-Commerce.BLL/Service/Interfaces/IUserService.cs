using E_Commerce.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.BLL.Service.Interfaces
{
    public interface IUserService
    {
        public Task<List<ApplicationUser>> GetAllAsync();

    }
}
