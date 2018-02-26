using PubLibIS_DAL.IoC;
using PubLibIS_BLL.Model;
using System.Collections.Generic;

namespace PubLibIS_BLL.Services
{
    public class AuthorService
    {
        private LibraryRepository repos;

        public AuthorService()
        {
            repos = new LibraryRepository();
        }
        public IEnumerable<Author> GetAllAuthors()
        {
            var authors = repos.AuthorRepository.Read();
            return Mappers.AuthorMapper.MapManyUp(authors);
        }
        
    }
}
