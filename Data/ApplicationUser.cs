using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LangPrac.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public int? Age { get; set; }
        public ICollection<UserLanguage>? UserLanguages { get; set; }
    }

}
        // @context.User.Identity?.Name!
