﻿using PubLibIS_DAL.Model;
using System.Collections.Generic;

namespace PubLibIS_DAL.Interfaces
{
    public interface IArticleRepository
    {
        int Create(Article article);
        Article Read(int articleId);
        IEnumerable<Article> Read();
        IEnumerable<Article> Read(int skip, int take);
        void Update(Article article);
        void Delete(int articleId);
    }
}
