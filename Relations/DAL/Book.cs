using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relations.DAL
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PageCount { get; set; }
        public int AuthorId { get; set; }
        public Author Authors { get; set; } 
        // Child üzerinden Parent'a ulaşmak için kullanılır. 
        // Kaldırılırsa Booktan Author'a ulaşamayız.
    }
}
