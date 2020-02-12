using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Models
{
    public class UserRateModel
    {
        //public int Id { get; set; }

        public int UserId { get; set; }
        public UserModel User { get; set; }

        public int RateId { get; set; }
        public RateModel Rate { get; set; }
    }
}
