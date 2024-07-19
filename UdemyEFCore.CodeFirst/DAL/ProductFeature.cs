using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyEFCore.CodeFirst.DAL
{
    public class ProductFeature
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

       // public int ProductId { get; set; }  // Foreign Key Child Entity'de tutulur.
        public Product Product { get; set; }
    }
}
