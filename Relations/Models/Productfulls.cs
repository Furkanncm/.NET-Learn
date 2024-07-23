using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relations.Models
{
    public class Productfulls
    {
        public string Category { get; set; }
        public string Product{ get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public int Value { get; set; }


    }
}
