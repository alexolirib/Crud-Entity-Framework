using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class.Domain.repositorio
{
    public interface IcollectionAuthor
    {
        List<Author> retornarTodosAuthor();

        Author retornarAuthorId(int id);
    }
}
