using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
        List<CategoryModel> GetAllCategory();
        List<ProductModel> GetAllProducts();
        List<ProductModel> GetProductByCat(int id);
        void AddToCart(int userId, int productId);
        List<CartModel> GetCartItems(int userId);
        void IncreaseQty(int userId, int productId);
        void DecreaseQty(int userId, int productId);
        int GetCartCount(int userId);
    }
}
