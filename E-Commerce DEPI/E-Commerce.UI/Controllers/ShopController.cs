using ApiConsume;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace E_Commerce.UI.Controllers
{
	public class ShopController : Controller
	{
		IApiCall apiCall;
        public ShopController(IApiCall apiCall)
        {
            this.apiCall = apiCall;
        }
        public async Task<IActionResult> Index()
		{
			return View(await apiCall.GetAllAsync<Product>("Product/GetAll"));
		}
	}
}
