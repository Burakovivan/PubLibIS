using PubLibIS.DAL.Models;
using System.Collections.Generic;

namespace PubLibIS.DAL.Interfaces
{
    public interface IBookRepository
    {
        int Create(Book book);
        int Count();
        Book Get(int bookId);
        IEnumerable<Book> GetList();
        IEnumerable<Book> GetList(int skip, int take);
        IEnumerable<Book> GetList(IEnumerable<int> idList);
        void Update(Book book);
        void Delete(int bookId);
    }
}
