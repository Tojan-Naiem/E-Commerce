using E_Commerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.DAL.DTO.Response
{
    public class BrandResponseDTO
    {
        public Status Status = Status.Active;
        public long Id { get; set; }
        public string? Name { get; set; }
    }
}
