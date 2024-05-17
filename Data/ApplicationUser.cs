using Microsoft.AspNetCore.Identity;

namespace LangPrac.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string? Nickname { get; set; }
        public ICollection<UserLanguage> UserLanguages { get; set; } = new List<UserLanguage>();
        public ICollection<PartnerRating> UserRatings { get; set; } = new List<PartnerRating>();
        public string? Country { get; set; }
        public string? Gender { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public byte[]? AvatarImage { get; set; }

        public int RatingCount => UserRatings?.Count ?? 0;
        public bool HasRatings => RatingCount > 0;

        public double AverageRating
        {
            get
            {
                if (UserRatings == null || !UserRatings.Any())
                    return 0;

                return Math.Round(UserRatings.Average(r => r.Rating), 2);
            }
        }
    }
}
