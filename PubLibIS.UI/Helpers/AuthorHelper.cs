using PubLibIS.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace PubLibIS.UI.Helpers
{
    public class AuthorHelper
    {
        private AuthorService service;

        public AuthorHelper(AuthorService service)
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