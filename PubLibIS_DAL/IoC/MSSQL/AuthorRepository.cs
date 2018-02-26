using PubLibIS_DAL.Interfaces;
using PubLibIS_DAL.Model;
using System.Collections.Generic;
using System.Linq;

namespace PubLibIS_DAL.IoC.MSSQL
{
    public class AuthorRepository: IAuthorRepository
    {
        private LibraryContext context;

        public AuthorRepository(LibraryContext context)
        {
            this.context = context;
        }

        public void Create(Author authtor)
        {
            context.Authors.Add(authtor);
            context.SaveChanges();
        }

        public void Delete(int authtorId)
        {
            var authtor = Read(authtorId);
            context.Authors.Remove(authtor);
            context.SaveChanges();
        }

        public Author Read(int authtorId)
        {
            return context.Authors.Find(authtorId);
        }

        public IEnumerable<Author> Read()
        {
            return context.Authors.ToList();
        }

        public void Update(Author authtor)
        {
            context.Entry(authtor).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }
    }
}
