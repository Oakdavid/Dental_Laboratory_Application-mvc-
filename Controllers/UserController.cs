using Dental_lab_Application_MVC_.Models.Dtos;
using Dental_lab_Application_MVC_.Models.Service.Interface;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Dental_lab_Application_MVC_.Models.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.CodeAnalysis.CSharp.Syntax;
//using Dental_lab_Application_MVC_.Models.ViewModels.UserView;

namespace Dental_lab_Application_MVC_.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserController(IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginRequestModel userLoginView)
        {
            var loginRequest = new LoginRequestModel
            {
                UserName = userLoginView.UserName,
                Password = userLoginView.Password,
            };
            var login = _userService.LoginByEmailOrByUserNameAndPassword(loginRequest);
            if (login == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password");
                return View(loginRequest);
            }
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, login.Id.ToString()),
                new Claim(ClaimTypes.Name, login.UserName),
                new Claim(ClaimTypes.Email, login.UserEmail),
                new Claim(ClaimTypes.Role, login.RoleName),
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties();

             HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, properties);

            if(login.RoleName == "HeadDoctor") 
            {
                return RedirectToAction("HeadDoctorDashBoard");
            }
            if (login.RoleName == "Doctor")
            {
                return RedirectToAction("DoctorDashBoard");  
            }
            if (login.RoleName == "Patient")
            {
                return RedirectToAction("PatientDashBoard");    
            }
            return View(login);

        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
        [Authorize(Roles = "Doctor")]
        [HttpGet]
        public IActionResult DoctorDashBoard(UserDto user)
        {
            return View(user);
        }

        [Authorize(Roles = "Patient")]
        [HttpGet]
        public IActionResult PatientDashBoard(UserDto user)
        {
            return View(user);
        }

        [Authorize(Roles = "HeadDoctor")]
        [HttpGet]
        public IActionResult HeadDoctorDashBoard(UserDto user)
        {
            return View(user);
        }


    }
}
