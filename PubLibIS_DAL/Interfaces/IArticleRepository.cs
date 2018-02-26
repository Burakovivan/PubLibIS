using PubLibIS_DAL.Model;
using System.Collections.Generic;

namespace PubLibIS_DAL.Interfaces
{
    public interface IArticleRepository
    {
        void Create(Article article);
        Article Read(int articleId);
        IEnumerable<Article> Read();
        void Update(Article article);
        void Delete(int articleId);
    }
}
