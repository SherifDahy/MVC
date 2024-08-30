using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using MVC1.Data;
using ViewModels;

namespace MVC1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext _context)
        {
            this._context = _context;
        }
        public IActionResult Index()
        {
            List<Employee> employees = new List<Employee>();
            List<Product> products = new List<Product>();
            employees = _context.Employees.Where(x=> x.UserId == Convert.ToInt32( Request.Cookies["Id"])).OrderByDescending(x => x.id).Take(10).Include(nameof(Employee.Department)).ToList();
            products = _context.Products.Where(x => x.UserId == Convert.ToInt32(Request.Cookies["Id"])).OrderByDescending(x=>x.Id).Take(3).Include(nameof(Product.Category)).ToList();
            HomeMixVM vm = new HomeMixVM()
            {
                employees = employees,
                products = products,
            };
            return View(vm);
        }
        


    }
}
