using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relations.DTOs
{
    public class FirstDTO
    {
        public string CategoryName { get; set; }

        public int TotalPrice { get; set; }

        public int TotalKdv { get; set; }

        public DateTime? CreatedTime { get; set; } = DateTime.Now;

    }

}
