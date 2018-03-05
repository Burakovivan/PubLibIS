using PubLibIS.DAL.Interfaces;
using PubLibIS.DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace PubLibIS.DAL.Repositories.EntityFramework
{
    public class PublishedBookRepository : IPublishedBookRepository
    {
        private LibraryEntityFrameworkContext context;

        public PublishedBookRepository(LibraryEntityFrameworkContext context)
        {
            this.context = context;
        }

        public int Create(PublishedBook pBook)
        {
           
            pBook.Book = context.Books.Find(pBook.Book.Id);
            pBook.PublishingHouse = context.PublishingHouses.Find(pBook.PublishingHouse.Id);
            context.PublishedBooks.Add(pBook);
            context.SaveChanges();
            return pBook.Id;
        }

        public void Delete(int pBookId)
        {
            var pBook = Read(pBookId);
            context.PublishedBooks.Remove(pBook);
        }

        public PublishedBook Read(int pBookId)
        {
            return context.PublishedBooks.Find(pBookId);
        }

        public IEnumerable<PublishedBook> ReadByBookId(int bookId)
        {
            return context.PublishedBooks.Where(pb => pb.Book.Id == bookId).OrderByDescending(x=>x.Id).ToList();
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
            current.PublishingHouse = context.PublishingHouses.Find(pBook.PublishingHouse.Id);
            context.Entry(current).CurrentValues.SetValues(pBook);
        }
    }
}
