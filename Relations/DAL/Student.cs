using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relations.DAL
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Teacher> TeacherList { get; set; } = new();//null hatası almamak için new() ile başlatıyoruz.
    } 
}
