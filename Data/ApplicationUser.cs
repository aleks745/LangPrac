using Microsoft.AspNetCore.Identity;

namespace LangPrac.Data
{
    public class ApplicationUser : IdentityUser
    {
        public DateOnly? DateOfBirth { get; set; }
        public ICollection<UserLanguage>? UserLanguages { get; set; }
    }
}
