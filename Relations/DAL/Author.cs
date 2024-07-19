using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relations.DAL
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Book Books { get; set; }
    }
}
