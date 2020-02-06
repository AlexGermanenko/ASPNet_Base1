using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class UserRateModel
    {
        public int UserId { get; set; }
        public UserModel User { get; set; }

        public int RateId { get; set; }
        public RateModel Rate { get; set; }
    }
}
