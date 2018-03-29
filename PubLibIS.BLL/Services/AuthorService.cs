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
            IEnumerable<Author> authors = db.Authors.GetList();
            return mapper.Map<IEnumerable<Author>, IEnumerable<AuthorViewModel>>(authors);
        }

        public AuthorViewModel GetAuthorViewModel(int id)
        {
            Author author = db.Authors.Get(id);
            return mapper.Map<Author, AuthorViewModel>(author);
        }

        public void DeleteAuthor(int id)
        {
            db.Authors.Delete(id);
            db.Save();
        }

        public void UpdateAuthor(AuthorViewModel author)
        {
            Author mappedAuthor = mapper.Map<AuthorViewModel, Author>(author);
            db.Authors.Update(mappedAuthor);
            db.Save();
        }

        public int CreateAuthor(AuthorViewModel author)
        {
            Author mappedAuthor = mapper.Map<AuthorViewModel, Author>(author);
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
            IEnumerable<Author> authorList = db.Authors.GetList(idList);

            foreach (Author author in authorList)
            {
                foreach (Book book in author.Books.Select(x => x.Book))
                {
                    book.PublishedBooks = db.PublishedBooks.GetPublishedBookByBookId(book.Id).ToList();
                }
            }
            string result = JsonConvert.SerializeObject(new AuthorJsonAggregator { Authors = authorList }, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, NullValueHandling = NullValueHandling.Ignore });
            return result;
        }

        public void SetJson(string json)
        {
            AuthorJsonAggregator deserRes = JsonConvert.DeserializeObject<AuthorJsonAggregator>(json);
            if (deserRes?.Authors == null)
            {
                return;
            }

            foreach (Author author in deserRes.Authors)
            {
                ICollection<AuthorInBook> books = author.Books;
                author.Books = null;
                var authorId = db.Authors.Create(author);

                foreach (AuthorInBook AuthorInBook in books)
                {
                    ICollection<PublishedBook> published = AuthorInBook.Book.PublishedBooks;
                    AuthorInBook.Book.PublishedBooks = null;
                    int bookId = db.Books.Create(AuthorInBook.Book);
                    AuthorInBook.Author = db.Authors.Get(authorId);
                    AuthorInBook.Book = db.Books.Get(bookId);
                    db.AuthorsInBooks.Create(AuthorInBook);


                    foreach (PublishedBook pBook in published)
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
