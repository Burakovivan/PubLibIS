using System.Collections.Generic;
using PubLibIS.ViewModels;

namespace PubLibIS.BLL.Interfaces
{
    public interface IArticleService
    {
        int CreateArticle(ArticleViewModel article);
        void DeleteArticle(int id);
        ArticleViewModel GetArticleViewModel(int id);
        IEnumerable<ArticleViewModel> GetArticleViewModelList();
        void UpdateArticle(ArticleViewModel article);
    }
}