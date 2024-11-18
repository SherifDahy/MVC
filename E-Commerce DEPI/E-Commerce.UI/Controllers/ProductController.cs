using ApiConsume;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using Services;

namespace E_Commerce.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        IApiCall apiCall;
        ImageService imageService;

        public ProductController(IApiCall apiCall)
        {
            imageService =  new ImageService();
            this.apiCall = apiCall;
        }
        public async Task<IActionResult> Index()
        {
            return View(await apiCall.GetAllAsync<Product>("Product/GetAll"));
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            SelectList selectListItems = new SelectList(await apiCall.GetAllAsync<Category>("Category/GetAll"), nameof(Category.CategoryId),nameof(Category.CategoryName));
            ViewBag.CategoryList = selectListItems;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(Product product,IFormFile formFile)
        {
            if (ModelState.IsValid)
            {
                product.ProductMainImage = await imageService.UploadFile(formFile);
                ApplicationResult<Product> applicationResult = await apiCall.PostReturnAsync<Product, ApplicationResult<Product>>("Product/Add", product);

                if (applicationResult.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    foreach (var error in applicationResult.Messages)
                    {
                        ModelState.AddModelError(error.Key,error.Value);
                    }

                }
            }
            return View();
        }
    }
}
