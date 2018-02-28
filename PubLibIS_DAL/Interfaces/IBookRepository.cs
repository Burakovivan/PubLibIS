using PubLibIS_DAL.Model;
using System.Collections.Generic;

namespace PubLibIS_DAL.Interfaces
{
    public interface IBookRepository
    {
        int Create(Book book);
        Book Read(int bookId);
        IEnumerable<Book> Read();
        IEnumerable<Book> Read(int skip, int take);
        IEnumerable<int> GetAuthorIdsByBook(int id);
        void Update(Book book);
        void Delete(int bookId);
    }
}
