using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relations.DAL
{
    public class ProdcutFeature
    {
        [ForeignKey("Product")]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public Product Product { get; set; }
    }
}
