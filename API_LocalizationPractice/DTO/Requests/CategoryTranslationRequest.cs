namespace API_LocalizationPractice.DTO.Requests
{
    public class CategoryTranslationRequest
    {
        public string Name { get; set; }
        public string Language { get; set; } = "en";
    }
}
