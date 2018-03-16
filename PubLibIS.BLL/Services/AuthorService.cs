using PubLibIS.DAL.UnitsOfWork;
using System.Collections.Generic;
using PubLibIS.ViewModels;
using PubLibIS.BLL.Interfaces;
using System.Linq;
using PubLibIS.DAL.Interfaces;
using AutoMapper;
using PubLibIS.DAL.Models;
using Newtonsoft.Json;
using PubLibIS.BLL.JsonModels;

namespace PubLibIS.BLL.Services
{
    public class AuthorService : IAuthorService
    {
        private IUnitOfWork db;
        private IMapper mapper;

        public AuthorService(IUnitOfWork uow, IMapper mapper)
        {
            db = uow;
            this.mapper = mapper;
        }

        public IEnumerable<AuthorViewModel> GetAuthorViewModelList()
        {
            var authors = db.Authors.Get();
            return mapper.Map<IEnumerable<Author>, IEnumerable<AuthorViewModel>>(authors);
        }

        public AuthorViewModel GetAuthorViewModel(int id)
        {
            var author = db.Authors.Get(id);
            return mapper.Map<Author, AuthorViewModel>(author);
        }

        public void DeleteAuthor(int id)
        {
            db.Authors.Delete(id);
            db.Save();
        }

        public void UpdateAuthor(AuthorViewModel author)
        {
            var mappedAuthor = mapper.Map<AuthorViewModel, Author>(author);
            db.Authors.Update(mappedAuthor);
            db.Save();
        }

        public int CreateAuthor(AuthorViewModel author)
        {
            var mappedAuthor = mapper.Map<AuthorViewModel, Author>(author);
            var newId = db.Authors.Create(mappedAuthor);
            db.Save();
            return newId;
        }

        public IEnumerable<int> GetAuthorIdListByBook(int id)
        {
            return db.Books.Get(id).Authors.Select(x => x.Author.Id);
        }

        public string GetJson(IEnumerable<int> idList)
        {
            var serializer = new JsonSerializer { Culture = new System.Globalization.CultureInfo("ru-RU"), NullValueHandling = NullValueHandling.Ignore };
            var authorList = db.Authors.Get(idList);

            foreach (var author in authorList)
                foreach (var book in author.Books.Select(x => x.Book))
                    book.PublishedBooks = db.PublishedBooks.GetPublishedBookByBookId(book.Id).ToList();

            var result = JsonConvert.SerializeObject(new AuthorJsonAggregator { Authors = authorList }, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, NullValueHandling = NullValueHandling.Ignore });
            return result;
        }

        public void SetJson(string json)
        {
            var deserRes = JsonConvert.DeserializeObject<AuthorJsonAggregator>(json);
            if (deserRes != null)
            {
                foreach (var author in deserRes.Authors)
                {
                    var books = author.Books;
                    author.Books = null;
                    int authorId = db.Authors.Create(author);

                    foreach (var AuthorInBook in books)
                    {
                        var published = AuthorInBook.Book.PublishedBooks;
                        AuthorInBook.Book.PublishedBooks = null;
                        int bookId = db.Books.Create(AuthorInBook.Book);
                        AuthorInBook.Author = db.Authors.Get(authorId);
                        AuthorInBook.Book = db.Books.Get(bookId);
                        db.AuthorsInBooks.Create(AuthorInBook);

                        foreach (var pBook in published)
                        {
                            pBook.Book = db.Books.Get(bookId);
                            int PubHouseId = db.PublishingHouses.Create(pBook.PublishingHouse);
                            pBook.PublishingHouse = db.PublishingHouses.Get(PubHouseId);
                            db.PublishedBooks.Create(pBook);
                        }
                    }
                }
                db.Save();
            }
        }
    }
}
