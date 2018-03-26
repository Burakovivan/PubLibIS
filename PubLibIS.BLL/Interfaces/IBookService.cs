using System.Collections.Generic;
using PubLibIS.ViewModels;

namespace PubLibIS.BLL.Interfaces
{
    public interface IBookService : IJsonProcessor
    {
        int CreateBook(BookViewModel book);
        int CreatePublication(PublishedBookViewModel pBook);
        IEnumerable<BookViewModel> GetBookViewModelList();
        IEnumerable<BookViewModelSlim> GetBookViewModelListSlim();
        BookCatalogViewModel GetBookCatalogViewModel(int skip, int take);
        BookViewModel GetBookViewModel(int id);
        PublishedBookViewModel GetPublication(int id);
        IEnumerable<PublishedBookViewModel> GetPublishedBookViewModelListByBook(int id);
        void UpdateBook(BookViewModel book);
        void UpdatePublication(PublishedBookViewModel pBook);
        void DeleteBook(int id);
        void DeletePublication(int id);
    }
}