using System.Collections.Generic;
using PubLibIS.ViewModels;

namespace PubLibIS.BLL.Interfaces
{
    public interface IAuthorService: IJsonProcessor
    {
        int CreateAuthor(AuthorViewModel author);
        void DeleteAuthor(int id);
        AuthorViewModel GetAuthorViewModel(int id);
        IEnumerable<AuthorViewModel> GetAuthorViewModelList();
        void UpdateAuthor(AuthorViewModel author);
        IEnumerable<int> GetAuthorIdListByBook(int id);
    }
}