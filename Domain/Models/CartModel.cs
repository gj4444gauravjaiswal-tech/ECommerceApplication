using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class CartModel
    {
        public int Cart_Id { get; set; }
        public int User_Id { get; set; }
        public int P_Id { get; set; }
        public int Quantity { get; set; }

        // Product details join ke liye
        public string P_Name { get; set; }
        public int P_Price { get; set; }
        public string P_Pic { get; set; }
    }
}
