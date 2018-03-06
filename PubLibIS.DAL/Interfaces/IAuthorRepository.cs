using PubLibIS.DAL.Models;
using System.Collections.Generic;

namespace PubLibIS.DAL.Interfaces
{
    public interface IAuthorRepository
    {
        int Create(Author auhtor);
        Author Get(int authorId);
        IEnumerable<Author> Get();
        IEnumerable<Author> Get(IEnumerable<int> ids);
        IEnumerable<Author> Get(int skip, int take);
        void Update(Author author);
        void Delete(int authorId);
    }
}
