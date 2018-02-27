using PubLibIS_DAL.Interfaces;
using PubLibIS_DAL.Model;
using System.Collections.Generic;
using System.Linq;

namespace PubLibIS_DAL.IoC.MSSQL
{
    public class AuthorInBookRepository : IAuthorInBookRepository
    {
        private LibraryContext context;

        public AuthorInBookRepository(LibraryContext context)
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
            context.SaveChanges();
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
            context.SaveChanges();
        }
    }
}
