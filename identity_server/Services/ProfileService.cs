using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace identity_server.Services
{
    public class ProfileService : IProfileService
    {


        protected UserManager<IdentityUser> _userManager;

        public ProfileService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);

            var claims = _userManager.GetClaimsAsync(user).GetAwaiter().GetResult();

            context.IssuedClaims.AddRange(claims);
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);
            context.IsActive = user != null;
        }


        //public Task GetProfileDataAsync(ProfileDataRequestContext context)
        //{
        //    IEnumerable<System.Security.Claims.Claim> claims = new List<Claim>()
        //    {
        //        new Claim(ClaimTypes.DateOfBirth,"01.01.2003"),
        //        new Claim(ClaimTypes.Role,"AdminU")
        //    };
        //    context.IssuedClaims.AddRange(claims);
        //    return Task.CompletedTask;
        //}

        //public Task IsActiveAsync(IsActiveContext context)
        //{
        //    context.IsActive = true;
        //    return Task.CompletedTask;
        //}
    }
}
