using ApiConsume;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace E_Commerce.UI.Controllers
{
    public class HomeController : Controller
    {
        IApiCall apiCall;
        public HomeController(IApiCall apiCall)
        {
            this.apiCall = apiCall;
        }
        public async Task<IActionResult> Index()
        {
			return View(await apiCall.GetAllAsync<Product>("Product/GetLast"));
        }
    }
}
