using PubLibIS.DAL.UoW;
using System.Collections.Generic;
using PubLibIS.ViewModels;
using PubLibIS.BLL.Interfaces;
using System.Linq;
using PubLibIS.DAL.Interfaces;

namespace PubLibIS.BLL.Services
{
    public class AuthorService : IAuthorService
    {
        private IUnitOfWork db;

        public AuthorService(IUnitOfWork uow)
        {
            db = uow;
        }

        public IEnumerable<AuthorViewModel> GetAuthorViewModelList()
        {
            var authors = db.Authors.Read();
            return Mappers.AuthorMapper.MapManyUp(authors);
        }

        public AuthorViewModel GetAuthorViewModel(int id)
        {
            var author = db.Authors.Read(id);
            return Mappers.AuthorMapper.MapOneUp(author);
        }

        public void Delete(int id)
        {
            db.Authors.Delete(id);
        }

        public void Update(AuthorViewModel author)
        {
            var mappedAuthor = Mappers.AuthorMapper.MapOneDown(author);
            db.Authors.Update(mappedAuthor);
        }

        public int Create(AuthorViewModel author)
        {
            var mappedAuthor = Mappers.AuthorMapper.MapOneDown(author);
            return db.Authors.Create(mappedAuthor);
        }

        public IEnumerable<int> GetAuthorIdListByBook(int id)
        {
            return db.Books.SelectMany(x => x.Authors.Select(y => y.Author.Id));
        }

        public IEnumerable<PublishingHouseViewModelSlim> GetPublishingHouseViewModelSlimList()
        {
            return db.PublishingHouses.Select(ph => new PublishingHouseViewModelSlim { Id = ph.Id, Description = $"{ph.Name} ({ph.Country}, {ph.City})" });
        }
    }
}
