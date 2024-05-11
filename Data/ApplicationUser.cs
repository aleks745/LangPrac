using Microsoft.AspNetCore.Identity;

namespace LangPrac.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string? Nickname { get; set; }
        public ICollection<UserLanguage>? UserLanguages { get; set; }
        public string? Country { get; set; }
        public string? Gender { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public byte[]? AvatarImage { get; set; }
    }
}
