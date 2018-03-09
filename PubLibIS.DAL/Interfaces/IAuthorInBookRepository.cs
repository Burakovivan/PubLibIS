using PubLibIS.DAL.Models;
using System.Collections.Generic;

namespace PubLibIS.DAL.Interfaces
{
    public interface IAuthorInBookRepository
    {
        int Create(AuthorInBook book);
        AuthorInBook Get(int ainbId);
        IEnumerable<AuthorInBook> GetByBookId(int bookId);
        IEnumerable<AuthorInBook> GetByBookId(IEnumerable<int> idList);
        IEnumerable<AuthorInBook> GetByAuthorId(int authorId);
        IEnumerable<AuthorInBook> GetByAuthorId(IEnumerable<int> idList);
        IEnumerable<AuthorInBook> Get();
        IEnumerable<AuthorInBook> Get(int skip, int take);
        void Update(AuthorInBook book);
        void Delete(int bookId);
    }
}
