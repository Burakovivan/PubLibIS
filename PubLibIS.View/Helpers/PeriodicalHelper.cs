using PubLibIS.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PubLibIS.BLL.Interfaces;
using System.Web.Mvc;

namespace PubLibIS.View.Helpers
{
    public class PeriodicalHelper
    {
        private IPeriodicalService service;

        public PeriodicalHelper(IPeriodicalService service)
        {
            this.service = service;
        }

        public int GetNextPeriodicalEditionNumber(int periodicalId)
        {
            return service.GetNextNumberByPeriodicalId(periodicalId);
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