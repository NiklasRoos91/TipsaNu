using Microsoft.AspNetCore.Mvc;

namespace TipsaNu.Api.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
