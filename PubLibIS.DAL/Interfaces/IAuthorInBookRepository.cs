using PubLibIS.DAL.ResponseModels;
using PubLibIS.Domain.Entities;
using System.Collections.Generic;

namespace PubLibIS.DAL.Interfaces
{
    public interface IAuthorInBookRepository
    {
        int Create(AuthorInBook book);
        AuthorInBook Get(int ainbId);
        IEnumerable<AuthorInBook> GetByBookId(int bookId);
        IEnumerable<GetAuthorInBookResponseModel> GetAuthorInBookResponseModelByBookId(int authorId);
        IEnumerable<AuthorInBook> GetByBookIdList(IEnumerable<int> idList);
        IEnumerable<AuthorInBook> GetByAuthorId(int authorId);
        IEnumerable<GetAuthorInBookResponseModel> GetAuthorInBookResponseModelByAuthorId(int authorId);
        IEnumerable<AuthorInBook> GetByAuthorIdList(IEnumerable<int> idList);
        IEnumerable<AuthorInBook> GetList();
        IEnumerable<AuthorInBook> GetList(int skip, int take);
        void Update(AuthorInBook book);
        void Delete(int bookId);
    }
}
