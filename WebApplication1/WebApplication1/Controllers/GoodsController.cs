using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Interfaces;
using WebApplication1.Mocks;
using WebApplication1.Models;
using ActionResult = Microsoft.AspNetCore.Mvc.ActionResult;
using Controller = Microsoft.AspNetCore.Mvc.Controller;
using JsonResult = Microsoft.AspNetCore.Mvc.JsonResult;

namespace WebApplication1.Controllers
{
    public class GoodsController : Controller
    {
        private IGoods _goods;

        public GoodsController(IGoods goods)
        {
            _goods = goods;
        }

        public IActionResult Product(int productId, int stars)
        {
            Product product = _goods.GetProductById(productId);

            if (stars!=0)
            {
                if (product.Rate == null)
                {
                    
                }
                else
                {
                    
                }
            }

            return View(product);
        }

        public IActionResult Index(int page = 1, SortState sortOrder = SortState.Name)
        {
            List<Product> goods = _goods.GetAllProducts();

            goods = SortGoods(sortOrder, goods);

            IndexViewModel indexViewModel = TakeIndexViewModel(page, goods);

            return View(indexViewModel);
        }

        private IndexViewModel TakeIndexViewModel(int page, List<Product> goods)
        {
            int pageSize = 6;
            int count = goods.Count();

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

        private List<Product> SortGoods(SortState sortOrder, List<Product> goods)
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