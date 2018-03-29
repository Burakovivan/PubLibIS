using PubLibIS.DAL.Models;
using System.Collections.Generic;

namespace PubLibIS.DAL.Interfaces
{
    public interface IAuthorRepository
    {
        Author Get(int authorId);
        IEnumerable<Author> GetList();
        IEnumerable<Author> GetList(IEnumerable<int> idList);
        IEnumerable<Author> GetList(int skip, int take);
        int Create(Author auhtor);
        void Update(Author author);
        void Delete(int authorId);
    }
}
