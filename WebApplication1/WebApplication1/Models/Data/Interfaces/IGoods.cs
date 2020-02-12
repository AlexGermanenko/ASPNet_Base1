using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Models.Data.Interfaces
{
    public interface IGoods
    {
        List<ProductModel> GetAllProducts();
        ProductModel GetProductById(int id);
        void ApplyRate(int productId, int stars, UserModel user);
    }
}
