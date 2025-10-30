using E_Commerce.Model;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.DTO.Request
{
    public class CategoryRequestDTO
    {
        public Status Status = Status.Active;
        public List<CategoryTranslationRequest> categoryTranslations { get; set; } 
            = new List<CategoryTranslationRequest>();
    }
}
