using System.Collections.Generic;
using PubLibIS.ViewModels;

namespace PubLibIS.BLL.Interfaces
{
    public interface IBookService
    {
        int Create(BookViewModel book);
        int CreatePublication(PublishedBookViewModel pBook);
        IEnumerable<BookViewModel> GetBookViewModelList();
        BookViewModel Get(int id);
        PublishedBookViewModel GetPublication(int id);
        IEnumerable<PublishedBookViewModel> GetPublishedBookViewModelListByBook(int id);
        void UpdateBook(BookViewModel book);
        void UpdatePublication(PublishedBookViewModel pBook);
        void DeleteBook(int id);
        void DeletePublication(int id);
    }
}