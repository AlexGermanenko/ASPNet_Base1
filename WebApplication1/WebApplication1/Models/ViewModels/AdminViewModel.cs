using System.Collections.Generic;
using AspNetBase.Models.Data.enums;

namespace AspNetBase.Models.ViewModels
{
    public class AdminViewModel
    {
        public UserModel AuthorizedUser { get; set; }
        public List<ProductModel> Goods { get; set; }
        public AdminAction Action { get; set; }
    }
}
