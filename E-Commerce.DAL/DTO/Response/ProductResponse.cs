using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace E_Commerce.DAL.DTO.Response
{
    public class ProductResponse
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public string MainImage { get; set; }
        public string MainImageUrl => $"https://localhost:7039/Images/{MainImage}";

    }
}
