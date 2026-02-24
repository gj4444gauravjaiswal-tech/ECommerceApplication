using Application.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _cs;
        public UserRepository(IConfiguration config)
        {
            _cs = config.GetConnectionString("constr");
        }
        public List<CategoryModel> GetAllCategory()
        {
            using(SqlConnection con = new SqlConnection(_cs))
            {
                SqlCommand cmd = new SqlCommand("sp_Category",con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@action",2);
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                List<CategoryModel> list = new List<CategoryModel>();
                while (sdr.Read())
                {
                    CategoryModel catmod = new CategoryModel()
                    {
                        C_ID = (int)sdr["c_id"],
                        C_Name = (string)sdr["c_name"],
                        C_Pic = (string)sdr["c_pic"]
                    };
                    list.Add(catmod);
                }
                return list;
            }
        }
        public List<ProductModel> GetAllProducts()
        {
            using (SqlConnection con = new SqlConnection(_cs))
            {
                SqlCommand cmd = new SqlCommand("sp_Product", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@action", 2);
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                List<ProductModel> list = new List<ProductModel>();
                while (sdr.Read())
                {
                    ProductModel prodmod = new ProductModel()
                    {
                        P_Id = (int)sdr["p_id"],
                        P_Name = (string)sdr["p_name"],
                        P_Cat = (int)sdr["cat_id"],
                        P_Desc = (string)sdr["p_desc"],
                        P_Price = (int)sdr["p_price"],
                        P_Pic = (string)sdr["p_image"]
                    };
                    list.Add(prodmod);
                }
                return list;
            }
        }
        public List<SignupModel> GetUserById()
        {
            using (SqlConnection con = new SqlConnection(_cs))
            {
                SqlCommand cmd = new SqlCommand("sp_User", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@action", 2);
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                List<SignupModel> list = new List<SignupModel>();
                while (sdr.Read())
                {
                    SignupModel usermod = new SignupModel()
                    {
                        Id = (int)sdr["u_id"],
                        Name = (string)sdr["u_name"],
                        Email = (string)sdr["u_email"],
                        Password = (string)sdr["u_password"],
                        Mobile = (long)sdr["u_mobile"],
                        Address = (string)sdr["u_address"],
                    };
                    list.Add(usermod);
                }
                return list;
            }
        }
    }
}
