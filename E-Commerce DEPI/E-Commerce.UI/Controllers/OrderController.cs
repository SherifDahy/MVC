using ApiConsume;
using Microsoft.AspNetCore.Mvc;
using Models;
using NuGet.Protocol;
using System.Security.Claims;

namespace E_Commerce.UI.Controllers
{
    public class OrderController : Controller
    {
        IApiCall apiCall;
        public OrderController(IApiCall apiCall)
        {
            this.apiCall = apiCall;
        }
        public async Task<IActionResult> Index(int id)
        {
            var temp = await apiCall.GetAllByIdAsync<IEnumerable<Order>>("Order/GetAll", id);
            return View(temp);
        }
        
        public async  Task<IActionResult> Add()
        {
            Order order = new Order()
            {
                UserId = int.Parse(User.Claims.FirstOrDefault(x=>x.Type == ClaimTypes.NameIdentifier).Value),
            };
            await apiCall.CreateAsync<Order>("Order/Add", order);
            return View();

        }

       
    }
}
