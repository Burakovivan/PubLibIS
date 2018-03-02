using PubLibIS.BLL.Interfaces;
using PubLibIS.DAL.UoW;
using System.Collections.Generic;
using PubLibIS.ViewModels;
using PubLibIS.DAL.Interfaces;

namespace PubLibIS.BLL.Services
{
    public class ArticleService : IArticleService
    {
        private IUnitOfWork db;

        public ArticleService(IUnitOfWork uow)
        {
            db = uow;
        }

        public IEnumerable<ArticleViewModel> GetArticleViewModelList()
        {
            var articles = db.Articles.Read();
            return Mappers.ArticleMapper.MapManyUp(articles);
        }

        public ArticleViewModel GetArticleViewModel(int id)
        {
            var article = db.Articles.Read(id);
            return Mappers.ArticleMapper.MapOneUp(article);
        }

        public void DeleteArticle(int id)
        {
            db.Articles.Delete(id);
        }

        public void UpdateArticle(ArticleViewModel article)
        {
            var mappedArticle = Mappers.ArticleMapper.MapOneDown(article);
            db.Articles.Update(mappedArticle);
        }

        public int CreateArticle(ArticleViewModel article)
        {
            var mappedArticle = Mappers.ArticleMapper.MapOneDown(article);
            return db.Articles.Create(mappedArticle);
        }

    }
}
