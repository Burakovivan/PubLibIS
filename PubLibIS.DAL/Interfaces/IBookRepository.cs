using PubLibIS.DAL.Models;
using System;
using System.Collections.Generic;

namespace PubLibIS.DAL.Interfaces
{
    public interface IBookRepository
    {
        int Create(Book book);
        Book Read(int bookId);
        IEnumerable<Book> Read();
        IEnumerable<Book> Read(int skip, int take);
        IEnumerable<T> Select<T>(Func<Book, T> selector);
        IEnumerable<T> SelectMany<T>(Func<Book, IEnumerable<T>> selector);
        IEnumerable<Book> Find(Func<Book, bool> predicate);
        void Update(Book book);
        void Delete(int bookId);
    }
}
