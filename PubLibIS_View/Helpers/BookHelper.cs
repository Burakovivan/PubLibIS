using PubLibIS_BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PubLibIS.Helpers
{
    public class BookHelper
    {
        private static ServiceContainer service;

        static BookHelper()
        {
            service = ServiceContainer.GetInstance();
        }

        public static MultiSelectList GetAuthorsSelectList(int id)
        {
            var authors = service.Author.GetAll();
            var authorOfBooks = service.Book.GetAuthorIdsByBook(id);
            return new MultiSelectList(authors, "Id", "FullName", authorOfBooks);
        }
        public static MultiSelectList GetAuthorsSelectList()
        {
            var authors = service.Author.GetAll();
            return new MultiSelectList(authors, "Id", "FullName");

        }

        public static SelectList GetPublishingHouseSelectList()
        {
            var houses = service.PublishingHouse.GetAll();
            return new SelectList(houses, "Id", "ListBoxInfo", houses.First());
        }

        public static SelectList GetPublishingHouseSelectList(int pHouseid)
        {
            var houses = service.PublishingHouse.GetAll();
            return new SelectList(houses, "Id", "ListBoxInfo", houses.SingleOrDefault(ph => ph.Id == pHouseid));
        }
    }
}