﻿namespace API_LocalizationPractice.Models.Category
{
    public class Category : BaseModel
    {
        public List<CategoryTranslation> CategoryTranslations { get; set; } = new List<CategoryTranslation>();
    }
}
