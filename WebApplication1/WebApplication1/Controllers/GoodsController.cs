using System;
using System.Collections.Generic;
using System.Linq;
using AspNetBase.Models;
using AspNetBase.Models.Data.enums;
using AspNetBase.Models.Data.Interfaces;
using AspNetBase.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Controller = Microsoft.AspNetCore.Mvc.Controller;


namespace AspNetBase.Controllers
{
    public class GoodsController : Controller
    {
        private IGoods _goods;
        private IUser _users;

        public GoodsController(IGoods goods, IUser users)
        {
            _goods = goods;
            _users = users;
        }
        
        public IActionResult Product(int productId)
        {
            ProductModel product = _goods.GetProductById(productId);

            ProductViewModel productView = new ProductViewModel(product, RateStatus.ok);

            return View(productView);
        }

        [Authorize]
        [HttpPost]
        public IActionResult RateProduct(int productId, int stars)
        {
            ProductModel product = _goods.GetProductById(productId);
            UserModel authorizedUser = _users.GetUserByEmail(HttpContext.User.Identity.Name);

            RateStatus rateStatus = RateStatus.ok;

            if (authorizedUser.isRated(product))
            {
                rateStatus = RateStatus.already_rate;
            }
            else
            {
                if (stars != 0)
                {
                    _goods.ApplyRate(productId, stars, authorizedUser);
                }
            }

            ProductViewModel productView = new ProductViewModel(product, rateStatus);

            return Json(productView);
        }

        
        public IActionResult Index(int page = 1, SortState sortOrder = SortState.Name)
        {
            List<ProductModel> goods = _goods.GetAllProducts();

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

            //пока не понял как отрефакторить switch
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
                    goods = goods.OrderBy(s => s.GetRate()).ToList();
                    break;
                case SortState.RateDesc:
                    goods = goods.OrderByDescending(s => s.GetRate()).ToList();
                    break;
            };

            return goods;
        }
    }
}