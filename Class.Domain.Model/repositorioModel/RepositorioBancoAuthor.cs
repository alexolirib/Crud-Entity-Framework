using Class.Domain.repositorio;
using ClassDomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class.Domain.Model.repositorioModel
{
    public class RepositorioBancoAuthor : IcollectionAuthor
    {

        public RepositorioBancoAuthor()
        {
            context = new ClassContext();
        }
        public Author retornarAuthorId(int id)
        {
            var context = new ClassContext();

            var query = context.Author.Where(c => c.id == id).FirstOrDefault();

            return query;
        }
        ClassContext context;
        public List<Author> retornarTodosAuthor()
        {

            var query = context.Author.OrderBy(c => c.Name).ToList();

            return query;
        }
    }
}
