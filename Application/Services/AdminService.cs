using Application.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AdminService : IAdminRepository
    {
        private readonly IAdminRepository _arepo;
        public AdminService(IAdminRepository arepo)
        {
            _arepo = arepo;
        }
        public void AddCategory(CategoryModel catmod)
        {
            if(catmod == null)
            {
                throw new NotImplementedException();
            }
             _arepo.AddCategory(catmod);
        }
        public List<CategoryModel> GetAllCategory()
        {
            return _arepo.GetAllCategory();
        }
        public void AddProduct(ProductModel pmod)
        {
            _arepo.AddProduct(pmod);
        }

        public List<ProductModel> GetAllProduct()
        {
            return _arepo.GetAllProduct();
        }
        List<SignupModel> IAdminRepository.GetAllUser()
        {
            return _arepo.GetAllUser();
        }
    }
}
