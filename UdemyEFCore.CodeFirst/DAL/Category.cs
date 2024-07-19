using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyEFCore.CodeFirst.DAL
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();

        // Category ile Product arasında 1-N ilişki kuruldu.
        // List de kullanılabilir. Fakat ICollection daha performanslı ve esnektir. 
    }
}
