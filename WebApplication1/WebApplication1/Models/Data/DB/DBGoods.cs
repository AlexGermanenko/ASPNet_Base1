﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.Data.Interfaces;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Models.Data.DB
{
    public class DBGoods : IGoods
    {
        private AppDBContext _context;

        public DBGoods(AppDBContext context)
        {
            _context = context;
        }

        public List<ProductModel> GetAllProducts() => new List<ProductModel>(_context.Product.Include(p => p.Rate).ToList());

        public ProductModel GetProductById(int id) => _context.Product.Include(p => p.Rate).FirstOrDefault(p => p.Id == id);

        public void ApplyRate(int productId, int stars, UserModel user)
        {
            ProductModel product = GetProductById(productId);


            UserRateModel userRateModel = new UserRateModel() { User = user, UserId = user.Id, Rate = product.Rate, RateId = product.Rate.Id };

            if (stars != 0)
            {

                if (product.Rate == null)
                {
                    product.Rate = new RateModel(productId, 0, 0, product);
                    product.Rate.RatesUsers.Add(userRateModel);
                }

                if (product.Rate.RatesUsers == null)
                {
                    product.Rate.RatesUsers = new List<UserRateModel>();
                    product.Rate.RatesUsers.Add(userRateModel);
                }

                if (user.RatesUsers == null)
                {
                    user.RatesUsers = new List<UserRateModel>();
                    user.RatesUsers.Add(userRateModel);
                }

                product.Rate.ApplyStars(stars);
                _context.Update(product);
                _context.Update(user);
                _context.Add(userRateModel);
                _context.SaveChanges();
            }
        }
    }
}
