using PubLibIS.DAL.Model;
using System.Collections.Generic;

namespace PubLibIS.DAL.Interfaces
{
    public interface IAuthorRepository
    {
        int Create(Author auhtor);
        Author Read(int authorId);
        IEnumerable<Author> Read();
        IEnumerable<Author> Read(int skip, int take);
        void Update(Author author);
        void Delete(int authorId);
    }
}
