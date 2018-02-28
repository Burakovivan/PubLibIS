using PubLibIS_DAL.Interfaces;
using PubLibIS_DAL.Model;
using System.Collections.Generic;
using System.Linq;

namespace PubLibIS_DAL.IoC.MSSQL
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
        public IEnumerable<int> GetAuthorIdsByBook(int id)
        {
            return context.Books.Find(id).Authors.Select(x => x.Author.Id);
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
            context.SaveChanges();
        }
        private void ResetAuthros(int id)
        {
            var ainbToRemove = Read(id).Authors;
            context.AuthorsInBooks.RemoveRange(ainbToRemove);
        }
    }
}
