namespace E_Commerce.Model.Category
{
    public class Category:BaseModel
    {
        public List<CategoryTranslation> categoryTranslations { get; set; } = new List<CategoryTranslation>();
    }
}
