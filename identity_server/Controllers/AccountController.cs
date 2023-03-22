
using identity_server.ViewModels;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace identity_server.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IIdentityServerInteractionService interactionService;
        private readonly SignInManager<IdentityUser> singInManager;

        public AccountController(IIdentityServerInteractionService interactionService,
            SignInManager<IdentityUser> singInManager,
            UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
            this.interactionService = interactionService;
            this.singInManager = singInManager;
        }
        [Route("[action]")]
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            var viewModel = new LoginViewModel
            {
                UserName = "user",
                ReturnUrl = returnUrl,
                Password = "user",
            };
            return View(viewModel);
        }
        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.FindByNameAsync(model.UserName);

            if (user == null)
            {
                ModelState.AddModelError("UserName", "user not find");
                return View(model);
            }

            var singIn = await singInManager.PasswordSignInAsync(user, model.Password, false, false);



            if (singIn.Succeeded)
            {
                if (string.IsNullOrWhiteSpace(model.ReturnUrl))
                {
                    ModelState.AddModelError("UserName", "Return url is empty");
                    return View(model);
                }
                else
                {
                    return Redirect(model.ReturnUrl);
                }
            }
            else
            {
                ModelState.AddModelError("Password", "Wrong password");
                return View(model);
            }
        }

        [Route("[action]")]
        public async Task<IActionResult> Logout(string logoutId)
        {
            await singInManager.SignOutAsync();
            var logoutRenault = await interactionService.GetLogoutContextAsync(logoutId);
            if (string.IsNullOrEmpty(logoutRenault.PostLogoutRedirectUri))
            {
                return RedirectToAction("Index", "Site"); 
            }

            return Redirect(logoutRenault.PostLogoutRedirectUri);
        }
    }
}
