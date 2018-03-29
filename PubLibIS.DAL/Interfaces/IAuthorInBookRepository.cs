using PubLibIS.DAL.Models;
using System.Collections.Generic;

namespace PubLibIS.DAL.Interfaces
{
    public interface IAuthorInBookRepository
    {
        int Create(AuthorInBook book);
        AuthorInBook Get(int ainbId);
        IEnumerable<AuthorInBook> GetByBookId(int bookId);
        IEnumerable<AuthorInBook> GetByBookIdList(IEnumerable<int> idList);
        IEnumerable<AuthorInBook> GetByAuthorId(int authorId);
        IEnumerable<AuthorInBook> GetByAuthorIdList(IEnumerable<int> idList);
        IEnumerable<AuthorInBook> GetList();
        IEnumerable<AuthorInBook> GetList(int skip, int take);
        void Update(AuthorInBook book);
        void Delete(int bookId);
    }
}
