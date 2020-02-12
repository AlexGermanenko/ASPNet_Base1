using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
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
    }
}
