using PubLibIS.BLL.Interfaces;
using PubLibIS.DAL.UnitsOfWork;
using System.Collections.Generic;
using PubLibIS.ViewModels;
using PubLibIS.DAL.Interfaces;
using AutoMapper;
using PubLibIS.DAL.Models;

namespace PubLibIS.BLL.Services
{
    public class ArticleService : IArticleService
    {
        private IUnitOfWork db;
        private IMapper mapper;

        public ArticleService(IUnitOfWork uow, IMapper mapper)
        {
            db = uow;
            this.mapper = mapper;
        }

        public IEnumerable<ArticleViewModel> GetArticleViewModelList()
        {
            var articles = db.Articles.Read();
            return mapper.Map<IEnumerable<Article>, IEnumerable< ArticleViewModel>>(articles);
        }

        public ArticleViewModel GetArticleViewModel(int id)
        {
            var article = db.Articles.Read(id);
            return mapper.Map<Article, ArticleViewModel>(article);
        }

        public void DeleteArticle(int id)
        {
            db.Articles.Delete(id);
            db.Save();
        }

        public void UpdateArticle(ArticleViewModel article)
        {
            var mappedArticle = mapper.Map<ArticleViewModel, Article>(article);
            db.Articles.Update(mappedArticle);
            db.Save();
        }

        public int CreateArticle(ArticleViewModel article)
        {
            var mappedArticle = mapper.Map<ArticleViewModel, Article>(article);
            int newId =  db.Articles.Create(mappedArticle);
            db.Save();
            return newId;
        }

    }
}
