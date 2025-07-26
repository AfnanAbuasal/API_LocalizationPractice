using API_LocalizationPractice.Models.Category;
using Microsoft.EntityFrameworkCore;

namespace API_LocalizationPractice.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryTranslation> CategoryTranslations { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=IMPLUTOGAL;Database=API_LocalizationPractice;Trusted_Connection=True;TrustServerCertificate=True");
        }
    }
}
