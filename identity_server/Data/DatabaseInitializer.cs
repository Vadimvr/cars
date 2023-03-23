using identity_server.Services;
using Microsoft.AspNetCore.Identity;

namespace identity_server.Data
{
    public static class DatabaseInitializer
    {
        public static void Init(IServiceProvider scopeServiceProvider)
        {
            var userManager = scopeServiceProvider.GetService<UserManager<IdentityUser>>();

            if (userManager == null) throw new ArgumentNullException(nameof(userManager), "userManager is null!");

            foreach (var item in Configuration.TestUser)
            {
                var user = new IdentityUser
                {
                    UserName = item.Username,

                };

                var result = userManager.CreateAsync(user, item.Password).GetAwaiter().GetResult();
                if (result.Succeeded)
                {
                    foreach (var claim in item.Claims)
                    {
                        userManager.AddClaimAsync(user, claim).GetAwaiter();
                    }
                }
            }
        }
    }
}
