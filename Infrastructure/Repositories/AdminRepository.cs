using Application.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly string _cs;
        public AdminRepository(IConfiguration configuration)
        {
            _cs = configuration.GetConnectionString("constr");
        }
        public void AddCategory(CategoryModel catmod)
        {
            if(catmod == null)
            {
                throw new NotImplementedException();
            }
            else
            {
                using (SqlConnection con = new SqlConnection(_cs))
                {
                    SqlCommand cmd = new SqlCommand("sp_Category", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@c_name", catmod.C_Name);
                    cmd.Parameters.AddWithValue("@c_pic", catmod.C_Pic);
                    cmd.Parameters.AddWithValue("@action", 1);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public List<CategoryModel> GetAllCategory()
        {
            using(SqlConnection con = new SqlConnection(_cs))
            {
                SqlCommand cmd = new SqlCommand("sp_Category", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@action",2);
                con.Open();
                List<CategoryModel> list = new List<CategoryModel>();
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    CategoryModel cat = new CategoryModel()
                    {
                        C_ID = (int)sdr["c_id"],
                        C_Name = (string)sdr["c_name"],
                        C_Pic = (string)sdr["c_pic"]
                    };
                    list.Add(cat);
                }
                return list;
            }
        }
        public void AddProduct(ProductModel pmod)
        {
            if (pmod == null)
                throw new ArgumentNullException(nameof(pmod));

            using (SqlConnection con = new SqlConnection(_cs))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Product", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_name", pmod.P_Name);
                    cmd.Parameters.AddWithValue("@cat_id", pmod.P_Cat);
                    cmd.Parameters.AddWithValue("@p_desc", pmod.P_Desc);
                    cmd.Parameters.AddWithValue("@p_price", pmod.P_Price);
                    cmd.Parameters.AddWithValue("@p_image", pmod.P_Pic);
                    cmd.Parameters.AddWithValue("@action", 1);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public List<ProductModel> GetAllProduct()
        {
            using(SqlConnection con = new SqlConnection(_cs))
            {
                SqlCommand cmd = new SqlCommand("sp_Product",con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@action",2);
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                List <ProductModel> list = new List<ProductModel>();
                while (sdr.Read())
                {
                    ProductModel pmod = new ProductModel()
                    {
                        P_Id = (int)sdr["p_id"],
                        P_Name = (string)sdr["p_name"],
                        P_Cat = (int)sdr["cat_id"],
                        C_Name = (string)sdr["c_name"],
                        P_Desc = (string)sdr["p_desc"],
                        P_Price = (int)sdr["p_price"],
                        P_Pic = (string)sdr["p_image"],
                    };
                    list.Add(pmod);
                }
                return list;
            }
        }
        public List<SignupModel> GetAllUser()
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
