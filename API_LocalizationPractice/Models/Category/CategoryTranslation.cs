namespace API_LocalizationPractice.Models.Category
{
    public class CategoryTranslation
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public string Language { get; set; } = "en";
        public int CategoryID { get; set; }
        public Category Category { get; set; }
    }
}
