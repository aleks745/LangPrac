using LangPrac.Data;
using Microsoft.AspNetCore.Identity;

namespace LangPrac.Components.Account
{
    internal sealed class IdentityUserAccessor
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IdentityRedirectManager _redirectManager;

        public IdentityUserAccessor(UserManager<ApplicationUser> userManager, IdentityRedirectManager redirectManager)
        {
            _userManager = userManager;
            _redirectManager = redirectManager;
        }

        public async Task<ApplicationUser> GetRequiredUserAsync(HttpContext context)
        {
            var user = await _userManager.GetUserAsync(context.User);

            if (user is null)
            {
                _redirectManager.RedirectTo("Account/Login");
            }

            return user;
        }
    }
}