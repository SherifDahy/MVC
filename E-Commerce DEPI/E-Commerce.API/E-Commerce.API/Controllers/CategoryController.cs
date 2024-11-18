using Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryController : BaseController
    {
        public CategoryController(IUnitOfWork unitOfWork):base(unitOfWork)
        {
            
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await UnitOfWork.Categories.GetAllAsync());
        }
    }
}
