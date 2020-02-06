using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Interfaces;

namespace WebApplication1.Models.Data.DB
{
    public class DBGoods : IGoods
    {
        private AppDBContext _context;

        public DBGoods(AppDBContext context)
        {
            _context = context;
        }

        public List<Product> GetAllProducts() => new List<Product>(_context.Product.Include(p => p.Rate).ToList());

        public Product GetProductById(int id) => _context.Product.Include(p => p.Rate).FirstOrDefault(p => p.Id == id);

        public void ApplyRate(int productId, int stars)
        {
            Product product = GetProductById(productId);

            if (stars != 0)
            {
                if (product.Rate == null)
                {
                    
                }
                else
                {

                }
            }
        }
    }
}
