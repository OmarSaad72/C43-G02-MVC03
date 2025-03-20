using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
    }
}
