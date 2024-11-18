using ApiConsume;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Security.Claims;

namespace E_Commerce.UI.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        IApiCall apiCall;
        public ShoppingCartController(IApiCall apiCall)
        {
            this.apiCall = apiCall;
        }
        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            return View(await apiCall.GetAllByIdAsync<ShoppingCart>("ShoppingCart/GetAll", id));
        }


        [HttpGet]
        public async Task<IActionResult> Add(int id)
        {
            ShoppingCart shoppingCart = new ShoppingCart()
            {
                ProductId = id,
                UserId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value),
            };

            await apiCall.CreateAsync<ShoppingCart>("ShoppingCart/Add", shoppingCart);
            return RedirectToAction(nameof(Index), new { id = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value) });
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (await apiCall.DeleteAsync<ShoppingCart>("ShoppingCart/Delete", id))
            {
                return RedirectToAction("Index", new { id = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value) });
            }
            return NotFound();
        }

        public async Task<IActionResult> Checkout()
        {
            var temp = await apiCall.GetAllByIdAsync<ShoppingCart>("ShoppingCart/GetAll",int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value));
            return View(temp);
        }

    }
}
