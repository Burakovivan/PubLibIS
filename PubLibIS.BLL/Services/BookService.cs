using PubLibIS.DAL.UnitsOfWork;
using System.Collections.Generic;
using PubLibIS.ViewModels;
using System.Linq;
using PubLibIS.BLL.Interfaces;
using PubLibIS.DAL.Interfaces;
using AutoMapper;
using PubLibIS.DAL.Models;

namespace PubLibIS.BLL.Services
{
    public class BookService : IBookService
    {
        private IUnitOfWork db;
        private IMapper mapper;

        public BookService(IUnitOfWork uow, IMapper mapper)
        {
            db = uow;
            this.mapper = mapper;
        }

        public IEnumerable<BookViewModel> GetBookViewModelList()
        {
            var books = db.Books.Read();
            return mapper.Map<IEnumerable<Book>, IEnumerable<BookViewModel>>(books);
        }

        public BookViewModel Get(int id)
        {
            var book = db.Books.Read(id);
            return mapper.Map<Book, BookViewModel>(book);
        }


        public void DeleteBook(int id)
        {
            db.Books.Delete(id);
            db.Save();
        }

        public void UpdateBook(BookViewModel book)
        {
            var mappedBook = mapper.Map<BookViewModel, Book>(book);

            db.Books.Update(mappedBook);
            db.Save();
        }

        public int Create(BookViewModel book)
        {
            var mappedBook = mapper.Map<BookViewModel, Book>(book);
            var newId = db.Books.Create(mappedBook);
            db.Save();
            return newId;
        }

        public int CreatePublication(PublishedBookViewModel publication)
        {
            var mappedPublication = mapper.Map<PublishedBookViewModel, PublishedBook>(publication);
            var newId = db.PublishedBooks.Create(mappedPublication);
            db.Save();
            return newId;
        }

        public IEnumerable<PublishedBookViewModel> GetPublishedBookViewModelListByBook(int id)
        {
            var publications = db.PublishedBooks.ReadByBookId(id);
            return mapper.Map<IEnumerable<PublishedBook>, IEnumerable<PublishedBookViewModel>>(publications);
        }

        public void DeletePublication(int id)
        {
            db.PublishedBooks.Delete(id);
            db.Save();
        }

        public PublishedBookViewModel GetPublication(int id)
        {
            var publication = db.PublishedBooks.Read(id);
            return mapper.Map<PublishedBook, PublishedBookViewModel>(publication);
        }

        public void UpdatePublication(PublishedBookViewModel publication)
        {
            var mappedPublication = mapper.Map<PublishedBookViewModel, PublishedBook>(publication);
            db.PublishedBooks.Update(mappedPublication);
            db.Save();
        }
    }
}
