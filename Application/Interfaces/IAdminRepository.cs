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
        SignupModel GetUserById(int id);
         void UpdateUser(SignupModel user);
         void DeleteUser(int id);
        CategoryModel GetCatById(int id);
        void UpdateCategory(CategoryModel catmod);
        void DeleteCategory(int id);
        ProductModel GetProductById(int id);
        void UpdateProduct(ProductModel product);
        void DeleteProduct(int id);
    }
}
