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

        public IEnumerable<AuthorViewModel> GetAll()
        {
            var authors = repos.AuthorRepository.Read();
            return Mappers.AuthorMapper.MapManyUp(authors);
        }

        public AuthorViewModel Get(int id)
        {
            var author = repos.AuthorRepository.Read(id);
            return Mappers.AuthorMapper.MapOneUp(author);
        }

        public void Delete(int id)
        {
            repos.AuthorRepository.Delete(id);
        }

        public void Update(AuthorViewModel author)
        {
            var mappedAuthor = Mappers.AuthorMapper.MapOneDown(author);
            repos.AuthorRepository.Update(mappedAuthor);
        }

        public void Create(AuthorViewModel author)
        {
            var mappedAuthor = Mappers.AuthorMapper.MapOneDown(author);
            repos.AuthorRepository.Create(mappedAuthor);
        }

    }
}
