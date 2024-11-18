using Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace E_Commerce.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        public ProductController(IUnitOfWork unitOfWork):base(unitOfWork)
        {
            
        }

        [HttpGet("GetLast")]
        public IActionResult GetLast()
        {
			return Ok(UnitOfWork.Products.GetAll().TakeLast(3));
        }
		[HttpGet("GetAll")]
		public IActionResult GetAll()
		{
			return Ok(UnitOfWork.Products.GetAll());
		}
        [HttpPost("Add")]
        public IActionResult Add(Product product) 
        {
            ApplicationResult<Product> applicationResult = new ApplicationResult<Product>();
            UnitOfWork.Products.Add(product);
            

            if (UnitOfWork.Save() == 1)
            {
                applicationResult.IsSuccess = true;
                applicationResult.Result = product;
            }
            else
            {
                applicationResult.IsSuccess = false;
            }

            
            return Ok(applicationResult);
            
        }
        
	}
}
