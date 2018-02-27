﻿using PubLibIS_DAL.Model;
using System.Collections.Generic;

namespace PubLibIS_DAL.Interfaces
{
    public interface IAuthorInBookRepository
    {
        int Create(AuthorInBook book);
        AuthorInBook Read(int bookId);
        IEnumerable<AuthorInBook> Read();
        IEnumerable<AuthorInBook> Read(int skip, int take);
        void Update(AuthorInBook book);
        void Delete(int bookId);
    }
}
