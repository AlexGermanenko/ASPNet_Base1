using System.Collections.Generic;

namespace AspNetBase.Models.ViewModels
{
    public class EditProductViewModel
    {
        public string Title { get; set; }
        public List<ProductModel> Goods { get; set; }
        public ProductModel Product { get; set; }
    }
}
