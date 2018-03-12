using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Class.Domain.repositorio;

namespace Class.Domain.Model.repositorioModel
{
    public class RepositorioMemoriaAuthor : IcollectionAuthor
    {

        public Author retornarAuthorId(int id)
        {
            var author = new Author
            {
                id = 1,
                Name = "João",
                Age = 35
            };
            return author; 
        }

        public List<Author> retornarTodosAuthor()
        {
            var author = new List<Author>
            {
                new Author
                {
                    id = 1,
                    Name = "Alvin",
                    Age = 23
                },
                new Author
                {
                    id = 2,
                    Name = "Davi",
                    Age = 20
                }
            };
            return author;
        }
    }
}
