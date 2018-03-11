using System;
using System.Collections.Generic;
using System.Data.Entity;
using Class.Domain;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDomainModel
{
    public class ClassContext: DbContext
    {
        public DbSet<Author> Author { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<Evaluation> Evaluation { get; set; }
        public DbSet<Student> Student { get; set; }
    }
}
