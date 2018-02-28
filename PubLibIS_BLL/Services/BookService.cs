using PubLibIS_DAL.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace PubLibIS_BLL.Services
{
    public class BookService
    {
        private LibraryRepository repos;

        public BookService()
        {
            repos = LibraryRepository.GetInstance();
        }

        public IEnumerable<BookViewModel> GetAll()
        {
            var books = repos.BookRepository.Read();
            return Mappers.BookMapper.MapManyUp(books);
        }

        public BookViewModel Get(int id)
        {
            var book = repos.BookRepository.Read(id);
            return Mappers.BookMapper.MapOneUp(book);
        }
        public IEnumerable<int> GetAuthorIdsByBook(int id)
        {
            return repos.BookRepository.GetAuthorIdsByBook(id);
        }

        public void Delete(int id)
        {
            repos.BookRepository.Delete(id);
        }

        public void Update(BookViewModel book)
        {
            var mappedBook = Mappers.BookMapper.MapOneDown(book);
           
            repos.BookRepository.Update(mappedBook);
        }

        public int Create(BookViewModel book)
        {
            var mappedBook = Mappers.BookMapper.MapOneDown(book);
            return repos.BookRepository.Create(mappedBook);
        }

    }
}
