using Microsoft.AspNetCore.Mvc;

namespace identity_server.Controllers
{

    public class SiteController : Controller
    {
       
        public IActionResult Index()
        {
            return View();
        }
    }

}
