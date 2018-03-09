using PubLibIS.DAL.Interfaces;
using PubLibIS.DAL.Models;
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
            var ainb = Get(ainbId);
            context.AuthorsInBooks.Remove(ainb);
        }

       

        public IEnumerable<AuthorInBook> Get()
        {
            return context.AuthorsInBooks.AsEnumerable();
        }

        public IEnumerable<AuthorInBook> Get(int skip, int take)
        {
            return context.AuthorsInBooks.Skip(skip).Take(take).AsEnumerable();
        }

        public void Update(AuthorInBook ainb)
        {
            var current = context.AuthorsInBooks.Find(ainb.Id);
            context.Entry(current).CurrentValues.SetValues(ainb);
        }
        public IEnumerable<AuthorInBook> GetByBookId(int bookId)
        {
            return context.Books.Find(bookId).Authors.ToList();
        }
        public IEnumerable<AuthorInBook> GetByBookId(IEnumerable<int> idList)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<AuthorInBook> GetByAuthorId(int authorId)
        {
            return context.Authors.Find(authorId).Books;
        }

        public IEnumerable<AuthorInBook> GetByAuthorId(IEnumerable<int> idList)
        {
            return context.AuthorsInBooks.Where(x => idList.Contains(x.Author.Id)).ToList();
        }

        public AuthorInBook Get(int ainbId)
        {
            return context.AuthorsInBooks.Find(ainbId);
        }
    }
}
