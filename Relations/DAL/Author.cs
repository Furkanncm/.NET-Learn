using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relations.DAL
{
    [Index(nameof(Name), nameof(Nickname))]
    [Index(nameof(Nickname))]
    [Index(nameof(Name))]
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public String Nickname { get; set; }
        public Book Books { get; set; }
    }
}
