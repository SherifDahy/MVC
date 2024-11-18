using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.UI.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
