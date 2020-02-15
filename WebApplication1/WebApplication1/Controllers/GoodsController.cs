using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.Data.Interfaces;
using WebApplication1.Mocks;
using WebApplication1.Models;
using WebApplication1.Models.ViewModels;
using ActionResult = Microsoft.AspNetCore.Mvc.ActionResult;
using Controller = Microsoft.AspNetCore.Mvc.Controller;
using JsonResult = Microsoft.AspNetCore.Mvc.JsonResult;
using Microsoft.AspNetCore.Authorization;
using WebApplication1.Models.Data.enums;


namespace WebApplication1.Controllers
{
    public class GoodsController : Controller
    {
        private IGoods _dbGoods;
        private IUser _users;

        public GoodsController(IGoods goods, IUser users)
        {
            _dbGoods = goods;
            _users = users;
        }
        
        public IActionResult Product(int productId)
        {
            ProductModel product = _dbGoods.GetProductById(productId);

            ProductViewModel productView = new ProductViewModel(product, RateStatus.ok);

            string points = productView.Rate.ToString();

            return View(productView);
        }

        [Authorize]
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IActionResult RateProduct(int productId, int stars)
        {
            ProductModel product = _dbGoods.GetProductById(productId);
            UserModel authorizedUser = _users.GetUserByEmail(HttpContext.User.Identity.Name);

            RateStatus rateStatus = RateStatus.ok;

            if (!authorizedUser.RatesUsers.Any(ru => ru.Rate != null && ru.Rate.ProductId == productId && ru.UserId == authorizedUser.Id))
            {
                if (stars != 0)
                {
                    _dbGoods.ApplyRate(productId, stars, authorizedUser);
                }
            }
            else
            {
                rateStatus = RateStatus.already_rate;
            }            

            ProductViewModel productView = new ProductViewModel(product, rateStatus);

            return Json(productView);
        }

        
        public IActionResult Index(int page = 1, SortState sortOrder = SortState.Name)
        {
            //return Content(User.);

            List<ProductModel> goods = _dbGoods.GetAllProducts();

            goods = SortGoods(sortOrder, goods);

            IndexViewModel indexViewModel = TakeIndexViewModel(page, goods);

            return View(indexViewModel);
        }

        private IndexViewModel TakeIndexViewModel(int page, List<ProductModel> goods)
        {
            int pageSize = 6;
            int count = goods.Count;

            goods = goods.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);

            ViewData["PageViewModel"] = pageViewModel;

            IndexViewModel indexViewModel = new IndexViewModel()
            {
                Goods = goods,
                PageViewModel = pageViewModel
            };

            return indexViewModel;
        }

        private List<ProductModel> SortGoods(SortState sortOrder, List<ProductModel> goods)
        {
            ViewData["NameSort"] = sortOrder == SortState.Name ? SortState.NameDesc : SortState.Name;
            ViewData["PriceSort"] = sortOrder == SortState.Price ? SortState.PriceDesc : SortState.Price;
            ViewData["RateSort"] = sortOrder == SortState.Rate ? SortState.RateDesc : SortState.Rate;

            switch (sortOrder)
            {
                case SortState.Name:
                    goods = goods.OrderBy(s => s.Name).ToList();
                    break;
                case SortState.NameDesc:
                    goods = goods.OrderByDescending(s => s.Name).ToList();
                    break;
                case SortState.Price:
                    goods = goods.OrderBy(s => s.Price).ToList();
                    break;
                case SortState.PriceDesc:
                    goods = goods.OrderByDescending(s => s.Price).ToList();
                    break;
                case SortState.Rate:
                    goods = goods.OrderBy(s => s.Rate).ToList();
                    break;
                case SortState.RateDesc:
                    goods = goods.OrderByDescending(s => s.Rate).ToList();
                    break;
            };

            return goods;
        }
    }
}