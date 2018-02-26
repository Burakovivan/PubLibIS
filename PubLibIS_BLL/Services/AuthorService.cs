using PubLibIS_DAL.IoC;
using System.Collections.Generic;
using ViewModels.Author;

namespace PubLibIS_BLL.Services
{
    public class AuthorService
    {
        private LibraryRepository repos;

        public AuthorService()
        {
            repos = LibraryRepository.GetInstance();
        }
        public IEnumerable<AuthorViewModel> GetAllAuthors()
        {
            var authors = repos.AuthorRepository.Read();
            return Mappers.AuthorMapper.MapManyUp(authors);
        }
        
    }
}
