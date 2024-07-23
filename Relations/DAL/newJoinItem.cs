using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relations.DAL
{
    public class newJoinItem
    {
        public String CategoryName { get; set; }
        public String ProductName { get; set; }
        public int? ProductPrice { get; set; }

        public newJoinItem(String CategoryName, String ProductName, int? ProductPrice)
        {
            this.CategoryName = CategoryName;
            this.ProductName = ProductName;
            this.ProductPrice = ProductPrice;
        }
    }
}
