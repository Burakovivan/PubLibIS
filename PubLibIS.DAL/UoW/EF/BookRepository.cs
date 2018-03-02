using PubLibIS.DAL.Interfaces;
using PubLibIS.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PubLibIS.DAL.UoW.EF
{
    public class BookRepository : IBookRepository
    {
        private LibraryContext context;

        public BookRepository(LibraryContext context)
        {
            this.context = context;
        }

        public int Create(Book book)
        {
            var bookAuthors = book.Authors.Select(a => a.Id);
            book.Authors = null;
            context.Books.Add(book);
            var authors = context.Authors.Where(x => bookAuthors.Contains(x.Id));
            foreach (var author in authors)
            {
                context.AuthorsInBooks.Add(new AuthorInBook
                {
                    Author = author,
                    Book = book
                });
            }

            return book.Id;
        }

        public void Delete(int bookId)
        {
            var book = Read(bookId);
            context.Books.Remove(book);
        }

        public Book Read(int bookId)
        {
            return context.Books.Find(bookId);
        }
        public IEnumerable<Book> Find(Func<Book, bool> predicate)
        { 
            return context.Books.Where(predicate).AsEnumerable();
        }

        public IEnumerable<Book> Read()
        {
            return context.Books.AsEnumerable();
        }

        public IEnumerable<Book> Read(int skip, int take)
        {
            return context.Books.Skip(skip).Take(take).AsEnumerable();
        }

        public void Update(Book book)
        {
            ResetAuthros(book.Id);
            var current = context.Books.Find(book.Id);
            var bookAuthors = book.Authors.Select(a => a.Id);
            var authors = context.Authors.Where(x => bookAuthors.Contains(x.Id));
            foreach (var author in authors)
            {
                context.AuthorsInBooks.Add(new AuthorInBook
                {
                    Author = author,
                    Book = current
                });
            }
            //context.Entry(current).State = System.Data.Entity.EntityState.Modified;
            context.Entry(current).CurrentValues.SetValues(book);
        }
        private void ResetAuthros(int id)
        {
            var ainbToRemove = Read(id).Authors;
            context.AuthorsInBooks.RemoveRange(ainbToRemove);
        }

        public IEnumerable<T> Select<T>(Func<Book, T> selector)
        {
            return context.Books.Select(selector).AsEnumerable();
        }

        public IEnumerable<T> SelectMany<T>(Func<Book, IEnumerable<T>> selector)
        {
            return context.Books.SelectMany(selector).AsEnumerable();
        }
    }
}
