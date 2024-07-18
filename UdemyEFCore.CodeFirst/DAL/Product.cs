using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyEFCore.CodeFirst.DAL
{
    //Product Modelimize ait özellikleri tanımladık
   //  [Table("MyProducts")]  Tablo ismi böyle de verilebilir.
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required,StringLength (100,MinimumLength =10)] // Validation yapılacak ise kullanılabilir.
        public string Name { get; set; }
        [Required , MaxLength(200)]
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public int? Stock { get; set; }

        public DateTime? CreatedTime { get; set; }

        public int? Barcode { get; set; }

    }
}
