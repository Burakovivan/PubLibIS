using PubLibIS.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PubLibIS.BLL.Interfaces;

namespace PubLibIS.UI.Helpers
{
    public class AuthorHelper
    {
        private IAuthorService service;

        public AuthorHelper(IAuthorService service)
        {
            this.service = service;
        }

        public MultiSelectList GetAuthorSelectList(int id)
        {
            var authors = service.GetAuthorViewModelList();
            var authorOfBooks = service.GetAuthorIdListByBook(id);
            return new MultiSelectList(authors, "Id", "FullName", authorOfBooks);
        }
        public MultiSelectList GetAuthorSelectList()
        {
            var authors = service.GetAuthorViewModelList();
            return new MultiSelectList(authors, "Id", "FullName");

        }

      

    }
}