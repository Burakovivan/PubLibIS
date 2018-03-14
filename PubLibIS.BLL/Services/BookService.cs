using PubLibIS.DAL.UnitsOfWork;
using System.Collections.Generic;
using PubLibIS.ViewModels;
using System.Linq;
using PubLibIS.BLL.Interfaces;
using PubLibIS.DAL.Interfaces;
using AutoMapper;
using PubLibIS.DAL.Models;
using Newtonsoft.Json;
using PubLibIS.BLL.JsonModels;

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
            var books = db.Books.Get();
            return mapper.Map<IEnumerable<Book>, IEnumerable<BookViewModel>>(books);
        }


        public IEnumerable<BookViewModelSlim> GetBookViewModelListSlim()
        {
            var books = db.Books.Get();
            return mapper.Map<IEnumerable<Book>, IEnumerable<BookViewModelSlim>>(books);
        }

        public BookCatalogViewModel GetBookCatalogViewModel(int skip, int take)
        {
            var books = db.Books.Get().OrderBy(b => b.Id).Skip(skip).Take(take);

            var result = new BookCatalogViewModel
            {
                Books = mapper.Map<IEnumerable<Book>, IEnumerable<BookViewModelSlim>>(books),
                Skip = skip,
                IsSeeMore = books.Count() < db.Books.Count(),
                HasNextPage = db.Books.Count() > skip + take
            };
            return result;
        }

        public BookViewModel Get(int id)
        {
            var book = db.Books.Get(id);
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
            var publications = db.PublishedBooks.GetByBookId(id);
            return mapper.Map<IEnumerable<PublishedBook>, IEnumerable<PublishedBookViewModel>>(publications);
        }

        public void DeletePublication(int id)
        {
            db.PublishedBooks.Delete(id);
            db.Save();
        }

        public PublishedBookViewModel GetPublication(int id)
        {
            var publication = db.PublishedBooks.Get(id);
            return mapper.Map<PublishedBook, PublishedBookViewModel>(publication);
        }

        public void UpdatePublication(PublishedBookViewModel publication)
        {
            var mappedPublication = mapper.Map<PublishedBookViewModel, PublishedBook>(publication);
            db.PublishedBooks.Update(mappedPublication);
            db.Save();
        }


        public string GetJson(IEnumerable<int> idList)
        {
            var BookList = db.Books.Get(idList).ToList();
            BookList.ForEach(book => book.PublishedBooks = db.PublishedBooks.GetByBookId(book.Id).ToList());
            var result = JsonConvert.SerializeObject(new BookJsonAggregator { Books = BookList }, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, NullValueHandling = NullValueHandling.Ignore });
            return result;
        }

        public void SetJson(string json)
        {
            var deserRes = JsonConvert.DeserializeObject<BookJsonAggregator>(json);

            if (deserRes != null)
            {
                foreach (var book in deserRes.Books)
                {
                    var aInBs = book.Authors;
                    var publishedBooks = book.PublishedBooks;
                    book.Authors = null;
                    book.PublishedBooks = null;
                    int bookId = db.Books.Create(book);
                    foreach (var ainb in aInBs)
                    {
                        var authorId = db.Authors.Create(ainb.Author);
                        ainb.Book = book;
                        db.AuthorsInBooks.Create(ainb);
                    }

                    foreach (var publishedBook in publishedBooks)
                    {
                        publishedBook.Book = book;
                        db.PublishedBooks.Create(publishedBook);
                    }
                }
                db.Save();
            }
        }


    }
}
