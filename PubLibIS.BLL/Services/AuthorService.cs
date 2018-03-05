using PubLibIS.DAL.UnitsOfWork;
using System.Collections.Generic;
using PubLibIS.ViewModels;
using PubLibIS.BLL.Interfaces;
using System.Linq;
using PubLibIS.DAL.Interfaces;
using AutoMapper;
using PubLibIS.DAL.Models;

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
            var authors = db.Authors.Read();
            return mapper.Map<IEnumerable<Author>,IEnumerable< AuthorViewModel>>(authors);
        }

        public AuthorViewModel GetAuthorViewModel(int id)
        {
            var author = db.Authors.Read(id);
            return mapper.Map<Author, AuthorViewModel>(author);
        }

        public void Delete(int id)
        {
            db.Authors.Delete(id);
            db.Save();
        }

        public void Update(AuthorViewModel author)
        {
            var mappedAuthor = mapper.Map<AuthorViewModel, Author>(author);
            db.Authors.Update(mappedAuthor);
            db.Save();
        }

        public int Create(AuthorViewModel author)
        {
            var mappedAuthor = mapper.Map<AuthorViewModel, Author>(author);
            var newId =  db.Authors.Create(mappedAuthor);
            db.Save();
            return newId;
        }

        public IEnumerable<int> GetAuthorIdListByBook(int id)
        {
            return db.Books.Read(id).Authors.Select(x => x.Author.Id);
        }

        
    }
}
