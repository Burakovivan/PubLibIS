using PubLibIS_DAL.Interfaces;
using PubLibIS_DAL.Model;
using System.Collections.Generic;
using System.Linq;

namespace PubLibIS_DAL.IoC.MSSQL
{
    public class BookRepository: IBookRepository
    {
        private LibraryContext context;

        public BookRepository(LibraryContext context)
        {
            this.context = context;
        }

        public int Create(Book book)
        {
            context.Books.Add(book);
            context.SaveChanges();
            return book.Id;
        }

        public void Delete(int bookId)
        {
            var book = Read(bookId);
            context.Books.Remove(book);
            context.SaveChanges();
        }

        public Book Read(int bookId)
        {
            return context.Books.Find(bookId);
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
            var current = context.Books.Find(book.Id);
            //current.Author = context.Authors.Find(book.Author.Id);
            //context.Entry(current).State = System.Data.Entity.EntityState.Modified;
            context.Entry(current).CurrentValues.SetValues(book);
            context.SaveChanges();
        }
    }
}
