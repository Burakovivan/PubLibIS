using PubLibIS_DAL.Interfaces;
using PubLibIS_DAL.Model;
using System.Collections.Generic;
using System.Linq;

namespace PubLibIS_DAL.IoC.MSSQL
{
    public class PublishedBookRepository : IPublishedBookRepository
    {
        private LibraryContext context;

        public PublishedBookRepository(LibraryContext context)
        {
            this.context = context;
        }

        public int Create(PublishedBook pBook)
        {
            context.PublishedBooks.Add(pBook);
            context.SaveChanges();
            return pBook.Id;
        }

        public void Delete(int pBookId)
        {
            var pBook = Read(pBookId);
            context.PublishedBooks.Remove(pBook);
            context.SaveChanges();
        }

        public PublishedBook Read(int pBookId)
        {
            return context.PublishedBooks.Find(pBookId);
        }

        public IEnumerable<PublishedBook> ReadByBookId(int bookId)
        {
            return context.PublishedBooks.Where(pb => pb.Book.Id == bookId).AsEnumerable();
        }

        public IEnumerable<PublishedBook> Read()
        {
            return context.PublishedBooks.AsEnumerable();
        }

        public IEnumerable<PublishedBook> Read(int skip, int take)
        {
            return context.PublishedBooks.Skip(skip).Take(take).AsEnumerable();
        }

        public void Update(PublishedBook pBook)
        {
            var current = Read(pBook.Id);
            context.Entry(current).CurrentValues.SetValues(pBook);
            context.SaveChanges();
        }
    }
}
