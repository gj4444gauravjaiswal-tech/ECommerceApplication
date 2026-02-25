using Application.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserRepository
    {
        private readonly IUserRepository _urepo;
        public UserService(IUserRepository urepo)
        {
            _urepo = urepo;
        }

        public void AddToCart(int userId, int productId)
        {
            throw new NotImplementedException();
        }

        public void DecreaseQty(int userId, int productId)
        {
            throw new NotImplementedException();
        }

        public List<CategoryModel> GetAllCategory()
        {
            return _urepo.GetAllCategory();
        }
        public List<ProductModel> GetAllProducts()
        {
            return _urepo.GetAllProducts();
        }

        public int GetCartCount(int userId)
        {
            throw new NotImplementedException();
        }

        public List<CartModel> GetCartItems(int userId)
        {
            throw new NotImplementedException();
        }

        public List<ProductModel> GetProductByCat(int id)
        {
            return _urepo.GetProductByCat(id);
        }

        public void IncreaseQty(int userId, int productId)
        {
            throw new NotImplementedException();
        }
    }
}
