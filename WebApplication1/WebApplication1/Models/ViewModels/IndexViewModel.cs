using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetBase.Models.ViewModels
{
    public class IndexViewModel
    {
        public List<ProductModel> Goods { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
