using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LangPrac.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Language> Languages { get; set; }
        public DbSet<UserLanguage> UserLanguages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserLanguage>()
                .HasKey(ul => new { ul.UserId, ul.LanguageId });

            modelBuilder.Entity<UserLanguage>()
                .HasOne(ul => ul.User)
                .WithMany(u => u.UserLanguages)
                .HasForeignKey(ul => ul.UserId);

            modelBuilder.Entity<UserLanguage>()
                .HasOne(ul => ul.Language)
                .WithMany(l => l.UserLanguages)
                .HasForeignKey(ul => ul.LanguageId);
        }

        public async Task<List<ApplicationUser>> SearchUsers(string userId, int languageId, string languageType)
        {
            return await Users
                .Where(u => u.Id != userId)
                .Where(u => u.UserLanguages.Any(ul =>
                    ul.LanguageId == languageId &&
                    ul.LanguageType != languageType))
                .ToListAsync();
        }
    }
}
