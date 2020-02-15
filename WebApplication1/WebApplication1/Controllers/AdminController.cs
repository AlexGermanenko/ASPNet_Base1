using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.Data.enums;
using WebApplication1.Models.Data.Interfaces;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Controllers
{
    
    public class AdminController : Controller
    {
        private IUser _dbUser;
        private IGoods _dbGoods;
        private UserModel _authorizedUser;

        public AdminController(IGoods goods, IUser users)
        {
            _dbUser = users;
            _dbGoods = goods;
        }

        [HttpPost]
        public ActionResult EditUser(UserModel model)
        {
            if (ModelState.IsValid) 
            {
                _dbUser.Modify(model);
            }
            else
            {
                ModelState.AddModelError("", "Некорректные данные");
            }

            ViewData["Title"] = AdminAction.EditUser.ToString();

            return View(model);
        }

        [HttpPost]
        public IActionResult AddProduct(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                _dbGoods.AddProduct(model);
            }
            else
            {
                ModelState.AddModelError("", "Некорректные данные");
            }

            ViewData["Title"] = AdminAction.AddProduct.ToString();

            return View();
        }

        [HttpPost]
        public IActionResult EditProduct(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                _dbGoods.ModifyProduct(model);
            }
            else
            {
                ModelState.AddModelError("", "Некорректные данные");
            }

            ViewData["Title"] = AdminAction.EditProduct.ToString();

            ViewBag.Goods = _dbGoods.GetAllProducts();

            return View();
        }

        [HttpGet]
        public IActionResult ClearRate(int rateId)
        {
            _dbGoods.ClearRate(rateId);

            return RedirectToAction("EditProduct");
        }

        [HttpPost]
        public IActionResult GetProduct(int productId)
        {
            ProductModel product = _dbGoods.GetProductById(productId);
            EditProductModel JsonProduct = new EditProductModel()
            {
                Id = product.Id,
                RateId = product.Rate == null? 0: product.Rate.Id,
                Name = product.Name,
                Description = product.Description,
                ImgURL = product.ImgURL,
                Price = product.Price,
                Rate = product.GetRate()
            };

            //return View(JsonProduct);
            return Json(JsonProduct);
        }

        [HttpGet]
        public IActionResult EditUser()
        {
            _authorizedUser = _dbUser?.GetUserByEmail(HttpContext.User.Identity.Name);

            ViewData["Title"] = AdminAction.EditUser.ToString();

            return View(_authorizedUser);
        }

        [HttpGet]
        public IActionResult EditProduct()
        {
            ViewData["Title"] = AdminAction.EditProduct.ToString();

            ViewBag.Goods = _dbGoods.GetAllProducts();

            return View();
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            ViewData["Title"] = AdminAction.AddProduct.ToString();

            return View();
        }
    }
}