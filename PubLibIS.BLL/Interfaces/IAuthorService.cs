using System.Collections.Generic;
using PubLibIS.ViewModels;

namespace PubLibIS.BLL.Interfaces
{
    public interface IAuthorService
    {
        int CreateAuthor(AuthorViewModel author);
        void DeleteAuthor(int id);
        AuthorViewModel GetAuthorViewModel(int id);
        IEnumerable<AuthorViewModel> GetAuthorViewModelList();
        void UpdateAuthor(AuthorViewModel author);
        IEnumerable<int> GetAuthorIdListByBook(int id);
        string GetAuthorJson(IEnumerable<int> idList); 
        void SetAuthorJson(string json);
    }
}