using PubLibIS.DAL.Interfaces;
using PubLibIS.DAL.ResponseModels;
using PubLibIS.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PubLibIS.DAL.Repositories.EntityFramework
{
    public class AuthorInBookRepository : IAuthorInBookRepository
    {
        private LibraryEntityFrameworkContext context;

        public AuthorInBookRepository(LibraryEntityFrameworkContext context)
        {
            this.context = context;
        }

        public int Create(AuthorInBook ainb)
        {
            context.AuthorsInBooks.Add(ainb);
            context.SaveChanges();
            return ainb.Id;
        }

        public void Delete(int ainbId)
        {
            AuthorInBook ainb = Get(ainbId);
            context.AuthorsInBooks.Remove(ainb);
        }


        public IEnumerable<AuthorInBook> GetList()
        {
            return context.AuthorsInBooks.AsEnumerable();
        }

        public IEnumerable<AuthorInBook> GetList(int skip, int take)
        {
            return context.AuthorsInBooks.Skip(skip).Take(take).AsEnumerable();
        }

        public void Update(AuthorInBook ainb)
        {
            AuthorInBook current = context.AuthorsInBooks.Find(ainb.Id);
            context.Entry(current).CurrentValues.SetValues(ainb);
        }
        public IEnumerable<AuthorInBook> GetByBookId(int bookId)
        {
            return context.AuthorsInBooks.Where(ainb => ainb.Book_Id == bookId);
        }
        public IEnumerable<AuthorInBook> GetByBookIdList(IEnumerable<int> idList)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<AuthorInBook> GetByAuthorId(int authorId)
        {
            return context.AuthorsInBooks.Where(ainb => ainb.Author_Id == authorId);
        }

        public IEnumerable<AuthorInBook> GetByAuthorIdList(IEnumerable<int> idList)
        {
            return context.AuthorsInBooks.Where(x => idList.Contains(x.Author.Id)).ToList();
        }

        public AuthorInBook Get(int ainbId)
        {
            return context.AuthorsInBooks.Find(ainbId);
        }

        public IEnumerable<GetAuthorInBookResponseModel> GetAuthorInBookResponseModelByAuthorId(int authorId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<GetAuthorInBookResponseModel> GetAuthorInBookResponseModelByBookId(int authorId)
        {
            throw new System.NotImplementedException();
        }
    }
}
