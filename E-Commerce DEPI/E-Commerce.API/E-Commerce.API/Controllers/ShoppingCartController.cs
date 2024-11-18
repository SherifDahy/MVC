using Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace E_Commerce.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ShoppingCartController : BaseController
    {
        public ShoppingCartController(IUnitOfWork unitOfWork):base(unitOfWork)
        {
            
        }

        [HttpGet("GetAll/{id}")]
        public IActionResult GetAll(int id)
        {
            return Ok(UnitOfWork.ShoppingCarts.FindAll(x=>x.UserId == id,includes:new string[]{nameof(Product)}));
        }

        [HttpPost("Add")]
        public IActionResult Add(ShoppingCart shoppingCart)
        {
            UnitOfWork.ShoppingCarts.Add(shoppingCart);
            UnitOfWork.Save();
            return Ok(shoppingCart);
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(int id)
        {
            var temp = UnitOfWork.ShoppingCarts.Find(x=>x.ShoppingCartId == id);
            UnitOfWork.ShoppingCarts.Delete(temp);
            UnitOfWork.Save();
            return Ok(temp);
        }


    }
}
