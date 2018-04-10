using PubLibIS.DAL.UnitsOfWork;
using System.Collections.Generic;
using PubLibIS.ViewModels;
using System.Linq;
using PubLibIS.DAL.Interfaces;
using AutoMapper;
using Newtonsoft.Json;
using PubLibIS.Domain.Entities;
using PubLibIS.DAL.ResponseModels;

namespace PubLibIS.BLL.Services
{
    public class BookService
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
            IEnumerable<GetBookResponseModel> books = db.Books.GetBookResponseModelList();
            return mapper.Map<IEnumerable<BookViewModel>>(books);
        }


        public IEnumerable<BookViewModelSlim> GetBookViewModelListSlim()
        {
            IEnumerable<Book> books = db.Books.GetBookList();
            return mapper.Map<IEnumerable<BookViewModelSlim>>(books);
        }

        public BookCatalogViewModel GetBookCatalogViewModel(int skip, int take)
        {
            if(take == 0)
            {
                take = db.Books.Count();
            }
            IEnumerable<GetBookResponseModel> books = db.Books.GetBookResponseModelList(skip,take);

            var result = new BookCatalogViewModel
            {
                Books = mapper.Map<IEnumerable<BookViewModelSlim>>(books),
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
            IEnumerable<AuthorInBook> mappedAuthor = mapper.Map<IEnumerable<Author>>(book.Authors).Select(a => new AuthorInBook { Author = a, Author_Id = a.Id });
            db.Books.Update(mappedBook, mappedAuthor);
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
            var BookList = db.Books.GetBookResponseModelList(idList).ToList();
            BookList.ForEach(book => book.PublishedBooks = db.PublishedBooks.GetPublishedBookByBookId(book.Id).ToList());
            var result = JsonConvert.SerializeObject(BookList, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, NullValueHandling = NullValueHandling.Ignore });
            return result;
        }

        public void SetJson(string json)
        {
            IEnumerable<GetBookResponseModel> deserRes = JsonConvert.DeserializeObject<IEnumerable<GetBookResponseModel>>(json);

            if(deserRes == null)
            {
                return;
            }
            foreach(GetBookResponseModel book in deserRes)
            {
                IEnumerable<GetAuthorInBookResponseModel> aInBs = book.Authors;
                IEnumerable<PublishedBook> publishedBooks = book.PublishedBooks;
                Book clearBook = mapper.Map<Book>(book);
                int bookId = db.Books.Create(clearBook);
                Book newBook = db.Books.Get(bookId);
                foreach(GetAuthorInBookResponseModel ainb in aInBs)
                {
                    var authorId = db.Authors.Create(ainb.Author);
                    ainb.Author = db.Authors.GetAuthor(authorId);
                    var newainb = new AuthorInBook { Author_Id = authorId, Book_Id = bookId };
                    db.AuthorsInBooks.Create(newainb);
                }

                foreach(PublishedBook publishedBook in publishedBooks)
                {
                    publishedBook.Book = newBook;
                    publishedBook.Book_Id = bookId;
                    db.PublishedBooks.Create(publishedBook);
                }
            }
            db.Save();

        }


    }
}
