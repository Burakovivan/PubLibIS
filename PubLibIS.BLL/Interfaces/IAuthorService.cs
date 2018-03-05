using System.Collections.Generic;
using PubLibIS.ViewModels;

namespace PubLibIS.BLL.Interfaces
{
    public interface IAuthorService
    {
        int Create(AuthorViewModel author);
        void Delete(int id);
        AuthorViewModel GetAuthorViewModel(int id);
        IEnumerable<AuthorViewModel> GetAuthorViewModelList();
        void Update(AuthorViewModel author);
        IEnumerable<int> GetAuthorIdListByBook(int id);
    }
}