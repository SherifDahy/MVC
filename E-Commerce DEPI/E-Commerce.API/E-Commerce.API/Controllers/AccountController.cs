using DomainModels;
using Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Repository;

namespace E_Commerce.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {

        #region Ctor
        JwtService JwtService;
        public AccountController(IUnitOfWork unitOfWork,IConfiguration configuration):base(unitOfWork)
        {
            JwtService = new JwtService(configuration);
        }
        #endregion

        #region Register
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            ApplicationResult<ApplicationUser> applicationResult = new ApplicationResult<ApplicationUser>();
            ApplicationUser applicationUser = new ApplicationUser()
            {
                UserName = dto.UserName,
                Email = dto.Email,
            };
            IdentityResult identityResult = await UnitOfWork.UserManager.CreateAsync(applicationUser, dto.Password);
            if (identityResult.Succeeded)
            {
                applicationResult.IsSuccess = true;
                await UnitOfWork.UserManager.AddToRoleAsync(applicationUser, "User");
                applicationResult.Result = applicationUser;
                return Ok(applicationResult);
            }
            else
            {
                applicationResult.IsSuccess = false;
                applicationResult.Messages = new Dictionary<string, string>();
                foreach (var error in identityResult.Errors)
                {
                    applicationResult.Messages.Add(error.Code, error.Description);
                }
                return Ok(applicationResult);

            }

        }


        #endregion

        #region AddRole

        [HttpPost("AddRole"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddRole(string role)
        {
            ApplicationResult<string> applicationResult = new ApplicationResult<string>();
            IdentityRole<int> identityRole = new IdentityRole<int>()
            {
                Name = role,
            };
            IdentityResult identityResult = await UnitOfWork.RoleManager.CreateAsync(identityRole);
            if (identityResult.Succeeded)
            {
                applicationResult.IsSuccess = true;
                return Ok(applicationResult);
            }
            else
            {
                applicationResult.IsSuccess = false;
                applicationResult.Messages = new Dictionary<string, string>();
                foreach (var error in identityResult.Errors)
                {
                    applicationResult.Messages.Add(error.Code, error.Description);
                }
                return Ok(applicationResult);
            }
        }
        #endregion

        #region Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            ApplicationResult<ApplicationAuth> applicationResult = new ApplicationResult<ApplicationAuth>();

            ApplicationUser applicationUser = await UnitOfWork.UserManager.FindByNameAsync(dto.UserName);
            if (applicationUser != null)
            {
                applicationResult.IsSuccess = await UnitOfWork.UserManager.CheckPasswordAsync(applicationUser, dto.Password);
                if (applicationResult.IsSuccess)
                {
                    var Roles = await UnitOfWork.UserManager.GetRolesAsync(applicationUser);
                    applicationResult.Result = new ApplicationAuth()
                    {
                        Name = applicationUser.UserName,
                        Token = JwtService.GenerateJSONWebToken(applicationUser, Roles[0]),
                        UserId = applicationUser.Id,
                        Role = Roles[0],
                    };
                    return Ok(applicationResult);
                }
            }
            applicationResult.Messages = new Dictionary<string, string>();
            applicationResult.Messages.Add("", "invalid User Name or Password.");
            return Ok(applicationResult);
        }
        #endregion

        [HttpGet("Profile/{id}")]
        public async Task<IActionResult> Profile([FromRoute]int id)
        {
            ApplicationUser applicationUser = await UnitOfWork.UserManager.FindByIdAsync(id.ToString());
            return Ok(applicationUser);
        }
    }
}
