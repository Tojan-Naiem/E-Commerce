using System.ComponentModel.DataAnnotations;

namespace E_Commerce.DTO.Request
{
    public class CategoryTranslationRequest
    {
        [Required]
        [MinLength(2, ErrorMessage = "The name must be more than 2 chars")]
        public string? Name { get; set; }

        public string Language { get; set; } = "en";
    }
}
