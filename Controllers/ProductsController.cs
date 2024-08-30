using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC1.Data;
using Models;
using ViewModels;

namespace MVC1.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            var applicationDbContext = _context.Products.Where(x => x.UserId == Convert.ToInt32(Request.Cookies["Id"])).Include(p => p.Category);
            return View( applicationDbContext.ToList());
        }

        public IActionResult Details(int id)
        {

            var product = _context.Products
                .Include(p => p.Category)
                .FirstOrDefault(m => m.Id == id);

            return View(product);
        }

        public IActionResult Create()
        {
           
            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                Product product = new Product()
                {
                    ProductName = productVM.ProductName,
                    ProductDesc = productVM.ProductDesc,
                    DefaultImage = productVM.DefaultImage,
                    Images = productVM.Images,
                    Price = productVM.Price,
                    CategoryId = productVM.CategoryId,
                    Category = _context.Categories.FirstOrDefault(x=>x.id == productVM.CategoryId),
                    UserId = Convert.ToInt32(Request.Cookies["Id"]),
                };
                _context.Add(product);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = _context.Categories.ToList();

            return View(productVM);
        }

        public IActionResult Edit(int id)
        {
            var product =  _context.Products.Find(id);

            ProductVM productVM = new ProductVM()
            {
                Id = id,
                ProductName = product.ProductName,
                ProductDesc = product.ProductDesc,
                DefaultImage = product.DefaultImage,
                Images = product.Images,
                Price = product.Price,
                CategoryId = product.CategoryId,
                
            };

            ViewBag.Categories = _context.Categories.ToList();
            return View(productVM);
        }

        [HttpPost]
        public IActionResult Edit(int id, ProductVM productVM)
        {

            
            if (ModelState.IsValid)
            {
                Product product = new Product()
                {
                    ProductName = productVM.ProductName,
                    DefaultImage= productVM.DefaultImage,
                    Images = productVM.Images,
                    Price = productVM.Price,
                    CategoryId = productVM.CategoryId,
                    ProductDesc= productVM.ProductDesc,
                    Id = id,
                    Category = _context.Categories.FirstOrDefault(x=>x.id == productVM.CategoryId),
                    UserId = Convert.ToInt16(Request.Cookies["Id"]),
                };
                _context.Update(product);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = _context.Categories.ToList();

            return View(productVM);
        }

        public IActionResult Delete(int? id)
        {

            var product = _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var product = _context.Products.Find(id);
            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        
    }
}
