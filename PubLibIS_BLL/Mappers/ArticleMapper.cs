using AutoMapper;
using PubLibIS_DAL.Model;
using System.Collections.Generic;
using ViewModels;

namespace PubLibIS_BLL.Mappers
{
    public static class ArticleMapper
    {
        public static ArticleViewModel MapOneUp(Article author)
        {
            return Mapper.Map<Article, ArticleViewModel>(author);
        }

        public static IEnumerable<ArticleViewModel> MapManyUp(IEnumerable<Article> authors)
        {
            return Mapper.Map<IEnumerable<Article>, IEnumerable<ArticleViewModel>>(authors);
        }

        public static Article MapOneDown(ArticleViewModel author)
        {
            return Mapper.Map<ArticleViewModel, Article>(author);
        }

        public static IEnumerable<Article> MapManyUp(IEnumerable<ArticleViewModel> authors)
        {
            return Mapper.Map<IEnumerable<ArticleViewModel>, IEnumerable<Article>>(authors);
        }

    }
}
