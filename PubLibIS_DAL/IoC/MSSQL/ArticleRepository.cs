using PubLibIS_DAL.Interfaces;
using PubLibIS_DAL.Model;
using System.Collections.Generic;
using System.Linq;

namespace PubLibIS_DAL.IoC.MSSQL
{
    public class ArticleRepository : IArticleRepository
    {
        private LibraryContext context;

        public ArticleRepository(LibraryContext context)
        {
            this.context = context;
        }

        public void Create(Article article)
        {
            context.Articles.Add(article);
            context.SaveChanges();
        }

        public void Delete(int articleId)
        {
            var article = Read(articleId);
            context.Articles.Remove(article);
            context.SaveChanges();
        }

        public Article Read(int articleId)
        {
            return context.Articles.Find(articleId);
        }

        public IEnumerable<Article> Read()
        {
            return context.Articles.AsEnumerable();
        }

        public void Update(Article article)
        {
            context.Entry(article).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }
    }
}
