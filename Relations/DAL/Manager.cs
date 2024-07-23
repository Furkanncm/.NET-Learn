using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relations.DAL
{
    public class Manager
    {
        public int Id { get; set; }
        public string Department { get; set; } 

        public Person Person { get; set; }

    }
}
