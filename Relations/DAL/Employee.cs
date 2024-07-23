using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relations.DAL
{
    public class Employee
    {
        public int Id { get; set; }
        public int Salary { get; set; }

        public Person Person { get; set; }
    }
}
