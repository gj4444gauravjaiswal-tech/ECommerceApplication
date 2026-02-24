using Application.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class HomeRepository : IHomeRepository
    {
        public readonly string _cs;
            public HomeRepository(IConfiguration configuration)
        {
            _cs = configuration.GetConnectionString("constr");
        }
        public void AddSignup(SignupModel reg)
        {
            if(reg == null)
            {
                throw new NotImplementedException();
            }
            using (SqlConnection con = new SqlConnection(_cs))
            {
                SqlCommand cmd = new SqlCommand("sp_User", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@u_name", reg.Name);
                cmd.Parameters.AddWithValue("@u_email", reg.Email);
                cmd.Parameters.AddWithValue("@u_password", reg.Password);
                cmd.Parameters.AddWithValue("@u_mobile", reg.Mobile);
                cmd.Parameters.AddWithValue("@u_address", reg.Address);
                cmd.Parameters.AddWithValue("@action", 1);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public SigninModel GetSignup(SigninModel login)
        {
            if (login == null)
                throw new ArgumentNullException(nameof(login));

            using (SqlConnection con = new SqlConnection(_cs))
            {
                using (SqlCommand cmd = new SqlCommand("sp_User", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@u_email", login.Email);
                    cmd.Parameters.AddWithValue("@u_password", login.Password);
                    cmd.Parameters.AddWithValue("@action", 5); // Login action

                    con.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.Read())
                        {
                            var user = new SigninModel
                            {
                                Email = sdr["u_email"].ToString(),
                                Password = sdr["u_password"].ToString(),
                                Role = sdr["u_role"].ToString()
                            };

                            return user;
                        }
                    }
                }
            }

            return null;
        }
    }
}
