using PubLibIS.DAL.Models;
using System.Collections.Generic;

namespace PubLibIS.DAL.Interfaces
{
    public interface IBookRepository
    {
        int Create(Book book);
        int Count();
        Book Get(int bookId);
        IEnumerable<Book> Get();
        IEnumerable<Book> Get(int skip, int take);
        IEnumerable<Book> Get(IEnumerable<int> idList);
        void Update(Book book);
        void Delete(int bookId);
    }
}
