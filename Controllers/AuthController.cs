using Microsoft.AspNetCore.Mvc;
using Models;
using MVC1.Data;
using ViewModels;

namespace MVC1.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AuthController(ApplicationDbContext context)
        {
            this._context = context;
        }
        public IActionResult Login()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(SignInVM signInVM) 
        {
            if(ModelState.IsValid == true)
            {
                
                User? user =  _context.Users.Where(x => x.Email == signInVM.Email && x.Password == signInVM.Password).FirstOrDefault();
                if (user != null)
                {
                    Response.Cookies.Append("Id", user.id.ToString());
                    Response.Cookies.Append("Email",user.Email);
                    Response.Cookies.Append("Name", user.Name);
                    return RedirectToAction("Index", "Home");

                }
            }
            ModelState.AddModelError("Password", "Email or Password is incorrect.");
            return View();
        }
        public IActionResult Signup()

        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Signup(SignUpVM userVM)
        {
            if(ModelState.IsValid == true)
            {
                User user = new User()
                {
                    Name = userVM.Name,
                    Email = userVM.Email,
                    Password = userVM.Password,
                };
                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }
            else {
                return View();

            }

        }

        public IActionResult MyProfile()
        {
            SignUpVM signUpVM = new SignUpVM()
            {
                
                Email = Request.Cookies["Email"],
                Name = Request.Cookies["Name"],
            };
            return View(signUpVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult MyProfile(SignUpVM signUpVM)
        {
            if (ModelState.IsValid == true)
            {
                User user = new User()
                {
                    id = Convert.ToInt16(Request.Cookies["Id"]),
                    Name = signUpVM.Name,
                    Email = signUpVM.Email,
                    Password = signUpVM.Password,
                };
                _context.Users.Update(user);
                _context.SaveChanges();
            }
            
            return View(signUpVM);
        }

        public IActionResult Signout()
        {
            Response.Cookies.Delete("Email");
            Response.Cookies.Delete("Name");
            Response.Cookies.Delete("Id");
            return RedirectToAction("Login");
        }

    }
}
