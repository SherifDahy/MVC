using Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace E_Commerce.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : BaseController
    {
        public OrderController(IUnitOfWork unitOfWork):base(unitOfWork) 
        {
            
        }
        [HttpPost("Add")]
        public IActionResult Add(Order order)
        {
            order.OrderDate = DateTime.Now;
            order.OrderShippingAddress = string.Empty;
            order.OrderShippingCost = 0;
            order.OrderIsShipped = false;
            order.OrderStatus = "";

            UnitOfWork.Orders.Add(order);
            UnitOfWork.Save();
            foreach(ShoppingCart cart in UnitOfWork.ShoppingCarts.FindAll(x=>x.UserId == order.UserId, includes: new[] { "Product" }))
            {
                UnitOfWork.OrderDets.Add(new OrderDet()
                {
                    OrderId = order.OrderId,
                    Discount = 0,
                    Price = cart.Product.ProductPrice,
                    ProductColor = cart.ProductColor,
                    ProductId = cart.ProductId,
                    ProductQuantity = cart.ProductQuantity,
                    ProductSize = "",
                });
                UnitOfWork.ShoppingCarts.Delete(cart);
            }
            UnitOfWork.Save();
            return Ok();
        }

        [HttpGet("GetAll/{id}")]
        public IActionResult GetAll([FromRoute]int id)
        {
            var temp = UnitOfWork.Orders.FindAll(x => x.UserId == id);
            return Ok(temp);
        }
    }
}
