using PubLibIS.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PubLibIS.BLL.Interfaces;

namespace PubLibIS.View
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

        public SelectList GetPublishingHouseSelectList()
        {
            var houses = service.GetPublishingHouseViewModelSlimList();
            return new SelectList(houses, "Id", "ListBoxInfo", houses.First());
        }

        public SelectList GetPublishingHouseSelectList(int? selectedPHouseid)
        {
            if (!selectedPHouseid.HasValue)
            {
                return GetPublishingHouseSelectList();
            }
            var houses = service.GetPublishingHouseViewModelSlimList();
            return new SelectList(houses, "Id", "ListBoxInfo", houses.SingleOrDefault(ph => ph.Id == selectedPHouseid));
        }

    }
}