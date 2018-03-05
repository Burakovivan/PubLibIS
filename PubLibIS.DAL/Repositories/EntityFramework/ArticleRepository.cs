using PubLibIS.DAL.Interfaces;
using PubLibIS.DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace PubLibIS.DAL.Repositories.EntityFramework
{
    public class ArticleRepository : IArticleRepository
    {
        private LibraryEntityFrameworkContext context;

        public ArticleRepository(LibraryEntityFrameworkContext context)
        {
            this.context = context;
        }

        public int Create(Article article)
        {
            context.Articles.Add(article);
            context.SaveChanges();
            return article.Id;
        }

        public void Delete(int articleId)
        {
            var article = Read(articleId);
            context.Articles.Remove(article);
        }

        public Article Read(int articleId)
        {
            return context.Articles.Find(articleId);
        }

        public IEnumerable<Article> Read()
        {
            return context.Articles.AsEnumerable();
        }

        public IEnumerable<Article> Read(int skip, int take)
        {
            return context.Articles.Skip(skip).Take(take).AsEnumerable();
        }

        public void Update(Article article)
        {
            var current = Read(article.Id);
            context.Entry(current).CurrentValues.SetValues(article);
        }
    }
}
