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
    public class AuthorService 
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
            IEnumerable<GetAuthorResponseModel> authorList = db.Authors.GetAuthorResponseModelList();
            return mapper.Map<IEnumerable<GetAuthorResponseModel>, IEnumerable<AuthorViewModel>>(authorList);
        }

        public AuthorViewModel GetAuthorViewModel(int id)
        {
            GetAuthorResponseModel author = db.Authors.GetAuthorResponseModel(id);
            return mapper.Map<GetAuthorResponseModel, AuthorViewModel>(author);
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
            return db.Books.GetBookResponseModel(id).Authors.Select(x => x.Author.Id);
        }

        public string GetJson(IEnumerable<int> idList)
        {
            var serializer = new JsonSerializer { Culture = new System.Globalization.CultureInfo("ru-RU"), NullValueHandling = NullValueHandling.Ignore };
            IEnumerable<GetAuthorResponseModel> authorList = db.Authors.GetAuthorResponseModelList(idList);

           
            string result = JsonConvert.SerializeObject(authorList , Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, NullValueHandling = NullValueHandling.Ignore });
            return result;
        }

        public void SetJson(string json)
        {
            var deserRes = JsonConvert.DeserializeObject<IEnumerable<GetAuthorResponseModel>>(json);
            if (deserRes == null)
            {
                return;
            }

            //foreach (GetAuthorResponseModel author in deserRes)
            //{
            //    IEnumerable<GetAuthorInBookResponseModel> books = author.Books;
            //    author.Books = null;
            //    Author clearAuthor = mapper.Map<Author>(author);
            //    var authorId = db.Authors.Create(clearAuthor);

            //    foreach (GetAuthorInBookResponseModel book in books)
            //    {
            //        IEnumerable<PublishedBook> published = book.Book.PublishedBooks;
            //        Book clearBook = mapper.Map<Book>(book);
            //        int bookId = db.Books.Create(clearBook);
            //        AuthorInBook ainb = new AuthorInBook
            //        {
            //            Author_Id = authorId,
            //            Book_Id = bookId
            //        };
            //        db.AuthorsInBooks.Create(ainb);


            //        foreach (PublishedBook pBook in published)
            //        {
            //            pBook.Book = db.Books.Get(bookId);
            //            int PubHouseId = db.PublishingHouses.Create(pBook.PublishingHouse);
            //            pBook.PublishingHouse = db.PublishingHouses.Get(PubHouseId);
            //            db.PublishedBooks.Create(pBook);
            //        }
            //    }
            //}
            db.Save();
        }
    }
}
