using PubLibIS.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;

namespace PubLibIS.UI.Helpers
{
    public class PeriodicalHelper
    {
        private PeriodicalService service;

        public PeriodicalHelper(PeriodicalService service)
        {
            this.service = service;
        }

        public int GetNextPeriodicalEditionNumber(int periodicalId)
        {
            return service.GetNextEditionNumberByPeriodicalId(periodicalId);
        }

        public SelectList GetPeriodicalTypeViewModelSelectList()
        {
            var typeList = service.GetPeriodicalTypeViewModelList();
            return new SelectList(typeList, "Id", "Name");
        }

        public SelectList GetPeriodicalTypeViewModelSelectList(int? typeId)
        {
            if (!typeId.HasValue)
                return GetPeriodicalTypeViewModelSelectList();

            var typeList = service.GetPeriodicalTypeViewModelList();
            return new SelectList(typeList, "Id", "Name", typeList.FirstOrDefault(t=>t.Id == typeId));

        }

    }
}