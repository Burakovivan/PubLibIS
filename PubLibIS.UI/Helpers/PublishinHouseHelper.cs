using PubLibIS.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PubLibIS.BLL.Interfaces;

namespace PubLibIS.UI.Helpers
{
    public class PublishingHouseHelper
    {
        private PublishingHouseService service;

        public PublishingHouseHelper(PublishingHouseService service)
        {
            this.service = service;
        }

        public SelectList GetPublishingHouseSelectList()
        {
            var houses = service.GetPublishingHouseViewModelSlimList();
            return new SelectList(houses, "Id", "Description", houses.FirstOrDefault());
        }

        public SelectList GetPublishingHouseSelectList(int? selectedPHouseid)
        {
            if (!selectedPHouseid.HasValue)
            {
                return GetPublishingHouseSelectList();
            }
            var houses = service.GetPublishingHouseViewModelSlimList();
            return new SelectList(houses, "Id", "Description", houses.SingleOrDefault(ph => ph.Id == selectedPHouseid));
        }
    }
}