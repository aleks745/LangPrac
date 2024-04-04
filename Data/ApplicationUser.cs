using Microsoft.AspNetCore.Identity;

namespace LangPrac.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public int? Age { get; set; }
    }

}
