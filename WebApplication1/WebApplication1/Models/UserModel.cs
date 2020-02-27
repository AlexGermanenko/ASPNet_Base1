using System.Collections.Generic;
using System.Linq;

namespace AspNetBase.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsAdmin { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public List<UserRateModel> RatesUsers { get; set; }

        public UserModel()
        {
            RatesUsers = new List<UserRateModel>();
        }

        public bool isRated(ProductModel product) => 
            RatesUsers.Any(ru => ru.Rate != null && ru.Rate.ProductId == product.Id && ru.UserId == Id);
        
    }
}
