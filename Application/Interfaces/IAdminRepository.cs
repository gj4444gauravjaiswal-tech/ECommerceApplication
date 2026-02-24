using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAdminRepository
    {
        void AddCategory(CategoryModel catmod);
        List<CategoryModel> GetAllCategory();
        void AddProduct(ProductModel pmod);
        List<ProductModel> GetAllProduct();
        List<SignupModel> GetAllUser();
    }
}
