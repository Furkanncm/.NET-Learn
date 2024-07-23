using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relations.DAL
{
   // [Keyless]
    public class ProductFulls
    {
        public int Product_Id { get; set; }

        public String CategoryName { get; set; }

        public String ProductName { get; set; }

        public int Price { get; set; }

        public String Value { get; set; }
    }
}
