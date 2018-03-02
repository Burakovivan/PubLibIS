using PubLibIS.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PubLibIS.BLL.Interfaces;

namespace PubLibIS.View
{
    public class PublishinHouseHelper
    {
        private IPublishingHouseService service;

        PublishinHouseHelper(IPublishingHouseService service)
        {
            this.service = service;
        }
        //TODO:??? Maybe remove?
    }
}