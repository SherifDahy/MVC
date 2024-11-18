using ApiConsume;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using ViewModels;
using Models;

namespace E_Commerce.UI.Controllers
{
	public class AccountController : Controller
	{
		// GET: AccountController
		IApiCall _api;
        public AccountController(IApiCall _api)
        {
			this._api = _api;
            
        }



        #region Login

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM vM)
        {
            if (ModelState.IsValid == true)
            {
                var result = await _api.PostReturnAsync<LoginVM, ApplicationResult<ApplicationAuth>>("Account/Login", vM);
                if (result.IsSuccess)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name,result.Result.Name),
                        new Claim(ClaimTypes.Role, result.Result.Role),
                        new Claim(ClaimTypes.NameIdentifier,result.Result.UserId.ToString())
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));



                    var options = new CookieOptions
                    {
                        HttpOnly = true,
                        Expires = DateTime.UtcNow.AddDays(30),
                    };
                    Response.Cookies.Append("token", result.Result.Name);
                    return RedirectToAction(actionName: "index", controllerName: "Home");
                }
                foreach(var error in result.Messages)
                {
                    ModelState.AddModelError(error.Key, error.Value);
                }
            }
            return View();
        }
        #endregion


        #region Regiter
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM vM)
        {
            if(ModelState.IsValid)
            {
                var temp =  await _api.PostReturnAsync<RegisterVM, ApplicationResult<ApplicationAuth>>("Account/Register", vM);

            }
            return View();
        } 
        #endregion

        [HttpGet]
        public async Task<IActionResult> profile(int id)
        {
            ApplicationUser applicationUser = await _api.GetByIdAsync<ApplicationUser>("Account/Profile",id);
            return View(applicationUser);
        }
    }
}
