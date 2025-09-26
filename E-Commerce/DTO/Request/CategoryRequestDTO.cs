using System.ComponentModel.DataAnnotations;

namespace E_Commerce.DTO.Request
{
    public class CategoryRequestDTO
    {
        [Required]
        [MinLength(2,ErrorMessage ="The name must be more than 2 chars")]
        public string? Name { get; set; }
    }
}
