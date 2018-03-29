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
            IEnumerable<Book> books = db.Books.GetList();
            return mapper.Map<IEnumerable<Book>, IEnumerable<BookViewModel>>(books);
        }


        public IEnumerable<BookViewModelSlim> GetBookViewModelListSlim()
        {
            IEnumerable<Book> books = db.Books.GetList();
            return mapper.Map<IEnumerable<Book>, IEnumerable<BookViewModelSlim>>(books);
        }

        public BookCatalogViewModel GetBookCatalogViewModel(int skip, int take)
        {
            if(take == 0)
            {
                take = db.Books.Count();
            }
            IEnumerable<Book> books = db.Books.GetList().OrderBy(b => b.Id).Skip(skip).Take(take);

            var result = new BookCatalogViewModel
            {
                Books = mapper.Map<IEnumerable<Book>, IEnumerable<BookViewModelSlim>>(books),
                Skip = skip,
                IsSeeMore = books.Count() < db.Books.Count(),
                HasNextPage = db.Books.Count() > skip + take
            };
            return result;
        }

        public BookViewModel GetBookViewModel(int id)
        {
            Book book = db.Books.Get(id);
            return mapper.Map<Book, BookViewModel>(book);
        }


        public void DeleteBook(int id)
        {
            db.Books.Delete(id);
            db.Save();
        }

        public void UpdateBook(BookViewModel book)
        {
            Book mappedBook = mapper.Map<BookViewModel, Book>(book);

            db.Books.Update(mappedBook);
            db.Save();
        }

        public int CreateBook(BookViewModel book)
        {
            Book mappedBook = mapper.Map<BookViewModel, Book>(book);
            int newId = db.Books.Create(mappedBook);
            db.Save();
            return newId;
        }

        public int CreatePublication(PublishedBookViewModel publication)
        {
            PublishedBook mappedPublication = mapper.Map<PublishedBookViewModel, PublishedBook>(publication);
            int newId = db.PublishedBooks.Create(mappedPublication);
            db.Save();
            return newId;
        }

        public IEnumerable<PublishedBookViewModel> GetPublishedBookViewModelListByBook(int id)
        {
            IEnumerable<PublishedBook> publications = db.PublishedBooks.GetPublishedBookByBookId(id);
            return mapper.Map<IEnumerable<PublishedBook>, IEnumerable<PublishedBookViewModel>>(publications);
        }

        public void DeletePublication(int id)
        {
            db.PublishedBooks.Delete(id);
            db.Save();
        }

        public PublishedBookViewModel GetPublication(int id)
        {
            PublishedBook publication = db.PublishedBooks.Get(id);
            return mapper.Map<PublishedBook, PublishedBookViewModel>(publication);
        }

        public void UpdatePublication(PublishedBookViewModel publication)
        {
            PublishedBook mappedPublication = mapper.Map<PublishedBookViewModel, PublishedBook>(publication);
            db.PublishedBooks.Update(mappedPublication);
            db.Save();
        }


        public string GetJson(IEnumerable<int> idList)
        {
            var BookList = db.Books.GetList(idList).ToList();
            BookList.ForEach(book => book.PublishedBooks = db.PublishedBooks.GetPublishedBookByBookId(book.Id).ToList());
            var result = JsonConvert.SerializeObject(new BookJsonAggregator { Books = BookList }, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, NullValueHandling = NullValueHandling.Ignore });
            return result;
        }

        public void SetJson(string json)
        {
            BookJsonAggregator deserRes = JsonConvert.DeserializeObject<BookJsonAggregator>(json);

            if(deserRes == null)
            {
                return;
            }
            foreach(Book book in deserRes.Books)
            {
                ICollection<AuthorInBook> aInBs = book.Authors;
                ICollection<PublishedBook> publishedBooks = book.PublishedBooks;
                book.Authors = null;
                book.PublishedBooks = null;
                int bookId = db.Books.Create(book);
                Book newbook = db.Books.Get(bookId);
                foreach(AuthorInBook ainb in aInBs)
                {
                    var authorId = db.Authors.Create(ainb.Author);
                    ainb.Author = db.Authors.Get(authorId);
                    var newainb = new AuthorInBook { Author_Id = authorId, Book_Id = bookId };
                    db.AuthorsInBooks.Create(newainb);
                }

                foreach(PublishedBook publishedBook in publishedBooks)
                {
                    publishedBook.Book = newbook;
                    publishedBook.Book_Id = newbook.Id;
                    db.PublishedBooks.Create(publishedBook);
                }
            }
            db.Save();

        }


    }
}
