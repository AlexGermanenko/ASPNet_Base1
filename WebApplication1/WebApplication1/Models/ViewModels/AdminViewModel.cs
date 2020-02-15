using System.Collections.Generic;
using WebApplication1.Models.Data.enums;
using WebApplication1.Models.Data.Interfaces;

namespace WebApplication1.Models.ViewModels
{
    public class AdminViewModel
    {
        public UserModel AuthorizedUser { get; set; }
        public List<ProductModel> Goods { get; set; }
        public AdminAction Action { get; set; }
    }
}
