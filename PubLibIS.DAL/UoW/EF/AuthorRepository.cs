using PubLibIS.DAL.Interfaces;
using PubLibIS.DAL.Model;
using System.Collections.Generic;
using System.Linq;

namespace PubLibIS.DAL.UoW.EF
{
    public class AuthorRepository: IAuthorRepository
    {
        private LibraryContext context;

        public AuthorRepository(LibraryContext context)
        {
            this.context = context;
        }

        public int Create(Author authtor)
        {
            context.Authors.Add(authtor);
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
