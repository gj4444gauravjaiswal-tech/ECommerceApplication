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
        public List<CategoryModel> GetAllCategory()
        {
            return _urepo.GetAllCategory();
        }
        public List<ProductModel> GetAllProducts()
        {
            return _urepo.GetAllProducts();
        }

        public SignupModel GetUserById()
        {
            throw new NotImplementedException();
        }
    }
}
