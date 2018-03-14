using PubLibIS.DAL.Models;
using System;
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
        IEnumerable<T> Select<T>(Func<Book, T> selector);
        IEnumerable<T> SelectMany<T>(Func<Book, IEnumerable<T>> selector);
        IEnumerable<Book> Find(Func<Book, bool> predicate);
        void Update(Book book);
        void Delete(int bookId);
    }
}
