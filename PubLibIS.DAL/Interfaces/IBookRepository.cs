using PubLibIS.DAL.ResponseModels;
using PubLibIS.Domain.Entities;
using System.Collections.Generic;

namespace PubLibIS.DAL.Interfaces
{
    public interface IBookRepository
    {
        int Create(Book book, IEnumerable<AuthorInBook> authorInBookList = null);
        int Count();
        Book Get(int bookId);
        GetBookResponseModel GetBookResponseModel(int bookId);
        IEnumerable<Book> GetBookList();
        IEnumerable<Book> GetBookList(int skip, int take);
        IEnumerable<Book> GetBookList(IEnumerable<int> idList);
        IEnumerable<GetBookResponseModel> GetBookResponseModelList();
        IEnumerable<GetBookResponseModel> GetBookResponseModelList(int skip, int take);
        IEnumerable<GetBookResponseModel> GetBookResponseModelList(IEnumerable<int> idList);
        void Update(Book book, IEnumerable<AuthorInBook> authorInBookList = null);
        void Delete(int bookId);
    }
}
