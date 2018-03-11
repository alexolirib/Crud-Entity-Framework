using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class.Domain
{
    public class Author
    {
        public int id { get; set; }
        public String Name { get; set; }
        public int Age { get; set; }
        public /*virtual  virtual é preciso se eu quiser usar o lazy Loading */List<Course> Course { get; set; }

        public Author()
        {
            Course = new List<Course>();
        }
    }
}
