using System;

namespace AspNetBase.Models.ViewModels
{
    public enum RateStatus
    {
        ok,
        already_rate 
    }

    public class ProductViewModel
    {
        public string Status { get; private set; }
        public string Name { get; private set; }
        public string ImgURL { get; private set; }
        public string Description { get; private set; }        

        public int Id { get; private set; }
        public float Rate { get; private set; }
        public uint Price { get; private set; }

        public ProductViewModel( ProductModel product, RateStatus rateStatus)
        {
            Name = product.Name;
            ImgURL = product.ImgURL;
            Description = product.Description;
            Id = product.Id;
            Price = product.Price;

            if (rateStatus == RateStatus.already_rate)
            {
                Status = "already_rate";
            }
            else
            {
                if (product.Rate == null)
                {
                    Rate = 0;
                }
                else
                {
                    Rate = GetRate(product.Rate);
                }
            }            
        }

        private float GetRate(RateModel rate)
        {
            float result = 0;

            if (rate.Votes != 0)
            {
                result = (float)Math.Round((float)rate.Stars / (float)rate.Votes, 1);
            }

            return result;
        }
    }
}
