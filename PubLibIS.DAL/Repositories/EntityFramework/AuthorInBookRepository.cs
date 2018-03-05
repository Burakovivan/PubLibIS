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
            var ainb = Read(ainbId);
            context.AuthorsInBooks.Remove(ainb);
        }

        public AuthorInBook Read(int ainbId)
        {
            return context.AuthorsInBooks.Find(ainbId);
        }

        public IEnumerable<AuthorInBook> Read()
        {
            return context.AuthorsInBooks.AsEnumerable();
        }

        public IEnumerable<AuthorInBook> Read(int skip, int take)
        {
            return context.AuthorsInBooks.Skip(skip).Take(take).AsEnumerable();
        }

        public void Update(AuthorInBook ainb)
        {
            var current = context.AuthorsInBooks.Find(ainb.Id);
            context.Entry(current).CurrentValues.SetValues(ainb);
        }
    }
}
