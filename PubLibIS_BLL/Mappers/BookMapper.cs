using AutoMapper;
using PubLibIS_DAL.Model;
using System.Collections.Generic;
using ViewModels;

namespace PubLibIS_BLL.Mappers
{
    public static class BookMapper
    {
        public static BookViewModel MapOneUp(Book book)
        {
            return Mapper.Map<Book, BookViewModel>(book);
        }

        public static IEnumerable<BookViewModel> MapManyUp(IEnumerable<Book> books)
        {
            return Mapper.Map<IEnumerable<Book>, IEnumerable<BookViewModel>>(books);
        }

        public static Book MapOneDown(BookViewModel book)
        {
            return Mapper.Map<BookViewModel, Book>(book);
        }

        public static IEnumerable<Book> MapManyUp(IEnumerable<BookViewModel> books)
        {
            return Mapper.Map<IEnumerable<BookViewModel>, IEnumerable<Book>>(books);
        }

    }
}
