using PubLibIS.DAL.Interfaces;
using PubLibIS.DAL.ResponseModels;
using PubLibIS.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PubLibIS.DAL.Repositories.EntityFramework
{
    public class AuthorRepository: IAuthorRepository
    {
        private LibraryEntityFrameworkContext context;

        public AuthorRepository(LibraryEntityFrameworkContext context)
        {
            this.context = context;
        }

        public int Create(Author authtor)
        {
            context.Authors.Add(authtor);
            context.SaveChanges();
            return authtor.Id;
        }

        public void Delete(int authtorId)
        {
            var authtor = Get(authtorId);
            context.Authors.Remove(authtor);
        }

        public Author Get(int authtorId)
        {
            return context.Authors.Find(authtorId);
        }

        public Author GetAuthor(int authorId)
        {
            throw new System.NotImplementedException();
        }

        public GetAuthorResponseModel GetAuthorResponseModel(int authorId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<GetAuthorResponseModel> GetAuthorResponseModelList()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<GetAuthorResponseModel> GetAuthorResponseModelList(IEnumerable<int> idList)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<GetAuthorResponseModel> GetAuthorResponseModelList(int skip, int take)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Author> GetList()
        {
            return context.Authors.ToList();
        }

        public IEnumerable<Author> GetList(IEnumerable<int> ids)
        {
            return context.Authors.Where(a => ids.Contains(a.Id)).ToList();
        }

        public IEnumerable<Author> GetList(int skip, int take)
        {
            return context.Authors.Skip(skip).Take(take).AsEnumerable();
        }

        public void Update(Author authtor)
        {
            var current = Get(authtor.Id);
            context.Entry(current).CurrentValues.SetValues(authtor);
        }
    }
}
