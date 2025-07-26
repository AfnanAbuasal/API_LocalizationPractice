namespace API_LocalizationPractice.DTO.Responses
{
    public class CategoryResponseDTO
    {
        public int ID { get; set; }
        public List<CategoryTranslationsResponse> CategoryTranslations { get; set; }
    }
}
