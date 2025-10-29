namespace E_Commerce.Model.Category
{
    public class CategoryTranslation
    {
        public int Id { get; set; }
        public int I { get; set; }
        public string? Name { get; set; }
        public string Language { get; set; } = "en";
  
        public Category Category { get; set; }
    }
}
