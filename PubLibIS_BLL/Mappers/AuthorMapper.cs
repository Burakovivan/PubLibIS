﻿using AutoMapper;
using PubLibIS_DAL.Model;
using System.Collections.Generic;
using ViewModels.Author;

namespace PubLibIS_BLL.Mappers
{
    public static class AuthorMapper
    {
        public static AuthorViewModel MapOneUp(Author author)
        {
            return Mapper.Map<Author, AuthorViewModel>(author);
        }

        public static IEnumerable<AuthorViewModel> MapManyUp(IEnumerable<Author> authors)
        {
            return Mapper.Map<IEnumerable<Author>, IEnumerable<AuthorViewModel>>(authors);
        }

        public static Author MapOneDown(AuthorViewModel author)
        {
            return Mapper.Map<AuthorViewModel, Author>(author);
        }

        public static IEnumerable<Author> MapManyUp(IEnumerable<AuthorViewModel> authors)
        {
            return Mapper.Map<IEnumerable<AuthorViewModel>, IEnumerable<Author>>(authors);
        }

    }
}
