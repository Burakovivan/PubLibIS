using PubLibIS.DAL.Interfaces;
using PubLibIS.DAL.Models;
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
            var authtor = Read(authtorId);
            context.Authors.Remove(authtor);
        }

        public Author Read(int authtorId)
        {
            return context.Authors.Find(authtorId);
        }

        public IEnumerable<Author> Read()
        {
            return context.Authors.ToList();
        }

        public IEnumerable<Author> Read(int skip, int take)
        {
            return context.Authors.Skip(skip).Take(take).AsEnumerable();
        }

        public void Update(Author authtor)
        {
            var current = Read(authtor.Id);
            context.Entry(current).CurrentValues.SetValues(authtor);
        }
    }
}
