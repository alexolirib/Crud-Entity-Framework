using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class.Domain
{
    public class Student
    {
        public int id { get; set; }
        public String Name { get; set; }
        public List<Evaluation> Evaluation { get; set; }

        public Student()
        {
            Evaluation = new List<Evaluation>(); 
        }
    }

}
