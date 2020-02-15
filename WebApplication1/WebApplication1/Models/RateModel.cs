using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Interfaces;

namespace WebApplication1.Models
{
    public class RateModel: IRate
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Stars { get; set; }
        public int Votes { get; set; }

        public ProductModel Product { get; set; }
        public List<UserRateModel> RatesUsers { get; set; }

        public RateModel()
        {
            RatesUsers = new List<UserRateModel>();
        }

        public RateModel(int productId, int stars, int votes, ProductModel product)
        {
            ProductId = productId;
            Stars = stars;
            Votes = votes;
            Product = product;
            RatesUsers = new List<UserRateModel>();
        }

        public float GetRate()
        {
            float rate = Convert.ToSingle(Stars) / Convert.ToSingle(Votes);

            return (float) Math.Round(rate, 1);
        }

        internal void ApplyStars(int stars)
        {
            Stars += stars;
            Votes++;
        }
    }
}
