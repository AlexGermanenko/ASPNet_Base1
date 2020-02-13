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
using WebApplication1.Models.Data.DB;
using Microsoft.AspNetCore.Authorization;
using WebApplication1.Models.Data.Interfaces;

namespace WebApplication1.Controllers
{
    public class UserController : Controller
    {
        private IUser _db;

        public UserController(IUser context)
        {
            _db = context;
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult CheckEmail(string email)
        {
            UserModel user = _db.GetUserByEmail(email);

            if (email == "admin@mail.ru" || email == "aaa@gmail.com")
                return Json(false);
            return Json(true);
        }

        [Authorize]
        public async Task<IActionResult> UserInfo()
        {
            UserInfoViewModel model = new UserInfoViewModel(_db, HttpContext.User.Identity.Name);
            return View(model);
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
                UserModel user = _db.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

                if (user != null)
                {
                    await Authenticate(model.Email);

                    return RedirectToAction("Index", "Goods");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
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
                UserModel user = _db.GetUserByEmail(HttpContext.User.Identity.Name);
                if (user == null)
                {
                    _db.AddUser(new UserModel { Email = model.Email, Password = model.Password, IsAdmin = false, Name = model.Name, RatesUsers = new List<UserRateModel>() });

                    await Authenticate(model.Email); // аутентификация

                    return RedirectToAction("Index", "Goods");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "User");
        }
    }
}