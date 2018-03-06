using PubLibIS.DAL.UnitsOfWork;
using System.Collections.Generic;
using PubLibIS.ViewModels;
using PubLibIS.BLL.Interfaces;
using System.Linq;
using PubLibIS.DAL.Interfaces;
using AutoMapper;
using PubLibIS.DAL.Models;
using Newtonsoft.Json;

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
            return db.Books.Read(id).Authors.Select(x => x.Author.Id);
        }

        public string GetAuthorJson(IEnumerable<int> idList)
        {
            db.TurnOffProxy();
            var authorProxyList = db.Authors.Get(idList);
            var result = JsonConvert.SerializeObject(authorProxyList, Formatting.Indented);
            db.TurnOnProxy();
            return result;
        }

        public void SetAuthorJson(string json)
        {
            throw new System.NotImplementedException();
        }
    }
}
