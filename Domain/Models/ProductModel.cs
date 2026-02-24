using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ProductModel
    {
        public int P_Id { get; set; }
        public string P_Name { get; set; }
        public int P_Cat { get; set; }
        public string P_Desc { get; set; }
        public int P_Price { get; set; }
        public string P_Pic { get; set; }
        public string C_Name { get; set; }
    }
}
