using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Mocks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class GoodsController : Controller
    {
        public IActionResult Index(SortState sortOrder = SortState.Name)
        {
            ViewBag.NameSortParm = sortOrder == SortState.Name ? SortState.NameDesc : SortState.Name;
            ViewBag.DateSortParm = sortOrder == SortState.Price ? SortState.PriceDesc : SortState.Price;
            ViewBag.DateSortParm = sortOrder == SortState.Rate ? SortState.RateDesc : SortState.Rate;

            List<Product> goods = new MockGoods().GetAllProducts().ToList();

            goods = sortOrder switch
                {
                SortState.NameDesc => goods.OrderByDescending(s => s.Name),
                SortState.Price => goods.OrderBy(s => s.Price),
                SortState.PriceDesc => goods.OrderByDescending(s => s.Price),
                SortState.Rate => goods.OrderBy(s => s.Rate),
                SortState.RateDesc => goods.OrderByDescending(s => s.Rate),
                _ => goods.OrderBy(s => s.Name),
                };

            return View(goods);
        }
    }
}