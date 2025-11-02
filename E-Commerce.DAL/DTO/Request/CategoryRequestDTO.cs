using E_Commerce.Model;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.DTO.Request
{
    public class CategoryRequestDTO
    {
        public Status Status = Status.Active;
        [Required]
        [MinLength(2, ErrorMessage = "The name must be more than 2 chars")]
        public string? Name { get; set; }
    }
}
