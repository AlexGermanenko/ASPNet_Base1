using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class IndexViewModel
    {
        public List<Product> Goods { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
