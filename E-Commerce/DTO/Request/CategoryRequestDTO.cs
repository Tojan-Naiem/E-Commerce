using E_Commerce.Model;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.DTO.Request
{
    public class CategoryRequestDTO
    {
        public Status Status = Status.Active;
        public List<CategoryTranslationRequest> CategoryTranslationRequests { get; set; } 
            = new List<CategoryTranslationRequest>();
    }
}
