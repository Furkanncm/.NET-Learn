using Microsoft.EntityFrameworkCore;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relations.DAL
{
    
    public class Product
    {
        public int Id { get; set; }

       // [Column(TypeName = "NVARCHAR")]
        public string Name { get; set; }

        public int Price { get; set; }

        public int DiscountPrice { get; set; }

        //  [Unicode(false)] Depolama alanı tasarrufu sağlar.
        public int Barcode { get; set; }

       // [NotMapped] Veritabanında oluşturulmaz.
        public int KDV { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)] 
        // Veritabanında bir alan oluşturulur ve bu alanın değeri veritabanı tarafından hesaplanır.
        public int PriceKdv { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        // Sadece ekleme işleminde çalışır. ve update işleminde güncellenmez.
        public DateTime? CreatedTime { get; set; } = DateTime.Now;
        public int CategoryId { get; set; }

        public Category Category { get; set; }
        public ProdcutFeature ProdcutFeature { get; set; }
    }
}
