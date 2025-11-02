using E_Commerce.Model;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.DTO.Response
{
    public class CategoryResponseDTO
    {
        public Status Status = Status.Active;
        public long Id { get; set; }
        public string? Name { get; set; }

    }
}
