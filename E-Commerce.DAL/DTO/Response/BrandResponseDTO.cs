using E_Commerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace E_Commerce.DAL.DTO.Response
{
    public class BrandResponseDTO
    {
        public Status Status = Status.Active;
        public long Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public string Image { get; set; }
        public string MainImageUrl => $"https://localhost:7039/Images/{MainImage}";

    }
}
