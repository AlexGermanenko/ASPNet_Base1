using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Models.ViewModels;
using WebApplication1.Models.Data.Interfaces;

namespace WebApplication1.Controllers
{
    public class UserController : Controller
    {
        private IUser _dbUser;

        public UserController(IUser user)
        {
            _dbUser = user;
        }

    [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                if (_dbUser.EmailIsExist(model?.Email))
                {

                    UserModel user = _dbUser.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

                    if (user != null)
                    {
                        await Authenticate(model.Email);

                        return RedirectToAction("Index", "Goods");
                    }

                    ModelState.AddModelError("", "Некорректный пароль");
                }
                else
                {
                    ModelState.AddModelError("", "Некорректный логин");
                }
                
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                UserModel user = _dbUser.GetUserByEmail(HttpContext.User.Identity.Name);
                if (user == null)
                {
                    _dbUser.AddUser(new UserModel
                    {
                        Email = model.Email,
                        Password = model.Password,
                        IsAdmin = false,
                        Name = model.Name,
                        RatesUsers = new List<UserRateModel>()
                    });

                    await Authenticate(model.Email);

                    return RedirectToAction("Index", "Goods");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        private async Task Authenticate(string userName)
        {
           
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "User");
        }
        
        [AcceptVerbs("Get", "Post")]
        public IActionResult CheckLoginEmail(string email)
        {
            return Json(_dbUser.EmailIsExist(email));
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult CheckRegisterEmail(string email)
        {
            return Json(!_dbUser.EmailIsExist(email));
        }

    }
}