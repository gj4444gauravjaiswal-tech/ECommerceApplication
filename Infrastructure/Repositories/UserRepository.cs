using Application.Interfaces;
using Domain.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public List<ProductModel> GetProductByCat()
        {
            throw new NotImplementedException();
        }

        public List<ProductModel> GetProductByCat(int id)
        {
            using (SqlConnection con = new SqlConnection(_cs))
            {
                SqlCommand cmd = new SqlCommand("sp_Product", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@action", 7);
                cmd.Parameters.AddWithValue("@cat_id", id);
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

        public void AddToCart(int userId, int productId)
        {
            using (SqlConnection con = new SqlConnection(_cs))
            {
                SqlCommand cmd = new SqlCommand("sp_Cart", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@action", 1);
                cmd.Parameters.AddWithValue("@user_id", userId);
                cmd.Parameters.AddWithValue("@p_id", productId);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<CartModel> GetCartItems(int userId)
        {
            using (SqlConnection con = new SqlConnection(_cs))
            {
                SqlCommand cmd = new SqlCommand("sp_Cart", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@action", 2);
                cmd.Parameters.AddWithValue("@user_id", userId);
                con.Open();

                SqlDataReader sdr = cmd.ExecuteReader();
                List<CartModel> list = new List<CartModel>();

                while (sdr.Read())
                {
                    list.Add(new CartModel
                    {
                        Cart_Id = (int)sdr["cart_id"],
                        User_Id = (int)sdr["user_id"],
                        P_Id = (int)sdr["p_id"],
                        Quantity = (int)sdr["quantity"],
                        P_Name = sdr["p_name"].ToString(),
                        P_Price = (int)sdr["p_price"],
                        P_Pic = sdr["p_image"].ToString()
                    });
                }
                return list;
            }
        }

        public void IncreaseQty(int userId, int productId)
        {
            using (SqlConnection con = new SqlConnection(_cs))
            {
                SqlCommand cmd = new SqlCommand("sp_Cart", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@action", 3);
                cmd.Parameters.AddWithValue("@user_id", userId);
                cmd.Parameters.AddWithValue("@p_id", productId);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DecreaseQty(int userId, int productId)
        {
            using (SqlConnection con = new SqlConnection(_cs))
            {
                SqlCommand cmd = new SqlCommand("sp_Cart", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@action", 4);
                cmd.Parameters.AddWithValue("@user_id", userId);
                cmd.Parameters.AddWithValue("@p_id", productId);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public int GetCartCount(int userId)
        {
            using (SqlConnection con = new SqlConnection(_cs))
            {
                SqlCommand cmd = new SqlCommand("sp_Cart", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@action", 5);
                cmd.Parameters.AddWithValue("@user_id", userId);
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar() ?? 0);
            }
        }

        //public List<SignupModel> GetUserById()
        //{
        //    using (SqlConnection con = new SqlConnection(_cs))
        //    {
        //        SqlCommand cmd = new SqlCommand("sp_User", con);
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@action", 2);
        //        con.Open();
        //        SqlDataReader sdr = cmd.ExecuteReader();
        //        List<SignupModel> list = new List<SignupModel>();
        //        while (sdr.Read())
        //        {
        //            SignupModel usermod = new SignupModel()
        //            {
        //                Id = (int)sdr["u_id"],
        //                Name = (string)sdr["u_name"],
        //                Email = (string)sdr["u_email"],
        //                Password = (string)sdr["u_password"],
        //                Mobile = (long)sdr["u_mobile"],
        //                Address = (string)sdr["u_address"],
        //            };
        //            list.Add(usermod);
        //        }
        //        return list;
        //    }
        //}
    }
}
