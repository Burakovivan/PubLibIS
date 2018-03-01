using PubLibIS_DAL.IoC;
using System.Collections.Generic;
using ViewModels;

namespace PubLibIS_BLL.Services
{
    public class PublishedBookService
    {
        private LibraryRepository repos;

        public PublishedBookService()
        {
            repos = LibraryRepository.GetInstance();
        }

        public IEnumerable<PublishedBookViewModel> GetAll()
        {
            var pBooks = repos.PublishedBookRepository.Read();
            return Mappers.PublishedBookMapper.MapManyUp(pBooks);
        }

        public PublishedBookViewModel Get(int id)
        {
            var book = repos.PublishedBookRepository.Read(id);
            return Mappers.PublishedBookMapper.MapOneUp(book);
        }

        public IEnumerable<PublishedBookViewModel> GetByBook(int id)
        {
            var publications = repos.PublishedBookRepository.ReadByBookId(id);
            return Mappers.PublishedBookMapper.MapManyUp(publications);
        }

        public void Delete(int id)
        {
            repos.PublishedBookRepository.Delete(id);
        }

        public void Update(PublishedBookViewModel book)
        {
            var mappedBook = Mappers.PublishedBookMapper.MapOneDown(book);
            repos.PublishedBookRepository.Update(mappedBook);
        }

        public int Create(PublishedBookViewModel book)
        {
            var mappedBook = Mappers.PublishedBookMapper.MapOneDown(book);
            return repos.PublishedBookRepository.Create(mappedBook);
        }
    }
}
