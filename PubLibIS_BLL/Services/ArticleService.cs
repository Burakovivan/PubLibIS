using PubLibIS_DAL.IoC;
using System.Collections.Generic;
using ViewModels;

namespace PubLibIS_BLL.Services
{
    public class ArticleService
    {
        private LibraryRepository repos;

        public ArticleService()
        {
            repos = LibraryRepository.GetInstance();
        }

        public IEnumerable<ArticleViewModel> GetAll()
        {
            var articles = repos.ArticleRepository.Read();
            return Mappers.ArticleMapper.MapManyUp(articles);
        }

        public ArticleViewModel Get(int id)
        {
            var article = repos.ArticleRepository.Read(id);
            return Mappers.ArticleMapper.MapOneUp(article);
        }

        public void Delete(int id)
        {
            repos.ArticleRepository.Delete(id);
        }

        public void Update(ArticleViewModel article)
        {
            var mappedArticle = Mappers.ArticleMapper.MapOneDown(article);
            repos.ArticleRepository.Update(mappedArticle);
        }

        public int Create(ArticleViewModel article)
        {
            var mappedArticle = Mappers.ArticleMapper.MapOneDown(article);
            return repos.ArticleRepository.Create(mappedArticle);
        }

    }
}
