using E_Commerce.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.DAL.DTO.Request
{
    public class BrandRequestDTO
    {
        public Status Status = Status.Active;
        [Required]
        [MinLength(2, ErrorMessage = "The name must be more than 2 chars")]
        public string? Name { get; set; }
    }
}
