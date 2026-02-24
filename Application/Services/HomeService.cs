using Application.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class HomeService : IHomeRepository
    {
        private readonly IHomeRepository _hrepo;
        public HomeService(IHomeRepository hrepo)
        {
            _hrepo = hrepo;
        }

        public void AddSignup(SignupModel reg)
        {
            _hrepo.AddSignup(reg);
        }

        public SigninModel GetSignup(SigninModel login)
        {
            return _hrepo.GetSignup(login);
        }
    }
}
