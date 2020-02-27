using AspNetBase.Models;
using AspNetBase.Models.Data.enums;
using AspNetBase.Models.Data.Interfaces;
using AspNetBase.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
    
    public class AdminController : Controller
    {
        private IUser _users;
        private IGoods _goods;

        public AdminController(IGoods goods, IUser users)
        {
            _users = users;
            _goods = goods;
        }

        [HttpPost]
        public ActionResult EditUser(UserModel model)
        {
            if (ModelState.IsValid) 
            {
                _users.Modify(model);
            }
            else
            {
                ModelState.AddModelError("", "Некорректные данные");
            }

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddProduct(ProductModel model)
        {
            _goods.AddProduct(model);

            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult EditProduct(EditProductViewModel model)
        {
            _goods.ModifyProduct(model.Product);

            EditProductViewModel editProductViewModel = new EditProductViewModel()
            {
                Title = AdminAction.EditProduct.ToString(),
                Goods = _goods.GetAllProducts()
            };

            return View(editProductViewModel);
        }

        [Authorize]
        [HttpGet]
        public IActionResult ClearRate(int rateId)
        {
            _goods.ClearRate(rateId);

            return RedirectToAction("EditProduct");
        }

        [HttpPost]
        public IActionResult GetProduct(int productId)
        {
            ProductModel product = _goods.GetProductById(productId);
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

            return Json(JsonProduct);
        }

        [HttpGet]
        public IActionResult EditUser()
        {
            UserModel authorizedUser = _users?.GetUserByEmail(HttpContext.User.Identity.Name);

            ViewData["Title"] = AdminAction.EditUser.ToString();

            return View(authorizedUser);
        }

        [HttpGet]
        public IActionResult EditProduct()
        {
            EditProductViewModel editProductViewModel = new EditProductViewModel()
            {
                Title = AdminAction.EditProduct.ToString(),
                Goods = _goods.GetAllProducts()
            };

        return View(editProductViewModel);
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            ViewData["Title"] = AdminAction.AddProduct.ToString();

            return View();
        }
    }