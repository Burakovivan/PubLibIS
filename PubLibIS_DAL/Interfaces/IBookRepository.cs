using PubLibIS_DAL.Model;
using System.Collections.Generic;

namespace PubLibIS_DAL.Interfaces
{
    public interface IBookRepository
    {
        void Create(Book book);
        Book Read(int bookId);
        IEnumerable<Book> Read();
        void Update(Book book);
        void Delete(int bookId);
    }
}
