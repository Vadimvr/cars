using identity_server.Services;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace identity_server.Data
{
    public static class DatabaseInitializer
    {
        public static void Init(IServiceProvider scopeServiceProvider)
        {
            var userManager = scopeServiceProvider.GetService<UserManager<IdentityUser>>();

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
