using PubLibIS.DAL.UoW;
using System.Collections.Generic;
using PubLibIS.ViewModels;
using System.Linq;
using PubLibIS.BLL.Interfaces;
using PubLibIS.DAL.Interfaces;

namespace PubLibIS.BLL.Services
{
    public class BookService : IBookService
    {
        private IUnitOfWork db;

        public BookService(IUnitOfWork uow)
        {
            db = uow;
        }

        public IEnumerable<BookViewModel> GetBookViewModelList()
        {
            var books = db.Books.Read();
            var temp = books.ToList();
            return Mappers.BookMapper.MapManyUp(temp);
        }

        public BookViewModel Get(int id)
        {
            var book = db.Books.Read(id);
            return Mappers.BookMapper.MapOneUp(book);
        }
        

        public void UpdateBook(int id)
        {
            db.Books.Delete(id);
        }

        public void UpdateBook(BookViewModel book)
        {
            var mappedBook = Mappers.BookMapper.MapOneDown(book);
           
            db.Books.Update(mappedBook);
        }

        public int Create(BookViewModel book)
        {
            var mappedBook = Mappers.BookMapper.MapOneDown(book);
            return db.Books.Create(mappedBook);
        }

        public int CreatePublication(PublishedBookViewModel publication)
        {
            var mappedPublication = Mappers.PublishedBookMapper.MapOneDown(publication);
            return db.PublishedBooks.Create(mappedPublication);
        }

        public IEnumerable<PublishedBookViewModel> GetPublishedBookViewModelListByBook(int id)
        {
            var publications = db.PublishedBooks.ReadByBookId(id);
            return Mappers.PublishedBookMapper.MapManyUp(publications);
        }

        public void DeletePublication(int id)
        {
            db.PublishedBooks.Delete(id);
        }

        public PublishedBookViewModel GetPublication(int id)
        {
            var publication = db.PublishedBooks.Read(id);
            return Mappers.PublishedBookMapper.MapOneUp(publication);
        }

        public void UpdatePublication(PublishedBookViewModel publication)
        {
            var mappedPublication = Mappers.PublishedBookMapper.MapOneDown(publication);
            db.PublishedBooks.Update(mappedPublication);
        }
    }
}
