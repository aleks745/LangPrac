using Microsoft.AspNetCore.Identity;

namespace LangPrac.Data
{
    public class ApplicationUser : IdentityUser
    {
        public int? Age { get; set; }
        public ICollection<UserLanguage>? UserLanguages { get; set; }
    }

}
        // @context.User.Identity?.Name!
