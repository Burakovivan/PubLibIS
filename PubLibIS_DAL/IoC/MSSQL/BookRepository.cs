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

        public void Create(Book book)
        {
            context.Books.Add(book);
            context.SaveChanges();
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

        public void Update(Book book)
        {
            context.Entry(book).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }
    }
}
