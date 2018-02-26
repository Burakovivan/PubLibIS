using PubLibIS_DAL.Model;
using System.Collections.Generic;

namespace PubLibIS_DAL.Interfaces
{
    public interface IAuthorRepository
    {
        void Create(Author auhtor);
        Author Read(int authorId);
        IEnumerable<Author> Read();
        void Update(Author author);
        void Delete(int authorId);
    }
}
