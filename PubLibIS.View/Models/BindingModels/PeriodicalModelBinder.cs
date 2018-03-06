using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PubLibIS.ViewModels;
using PubLibIS.ViewModels.Util;

namespace PubLibIS.View.Models.BindingModels
{
    public class PeriodicalModelBinder : IModelBinder
    {

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var valueProvider = bindingContext.ValueProvider;

            string sPeriodicalId = valueProvider.GetValue("Id")?.AttemptedValue;
            int.TryParse(sPeriodicalId, out int periodicalId);

            string name = (string)valueProvider.GetValue("Name")?.ConvertTo(typeof(string));
            string issn = (string)valueProvider.GetValue("ISSN")?.ConvertTo(typeof(string));
            bool isPub = (bool)valueProvider.GetValue("IsPublished")?.ConvertTo(typeof(bool));
            string sType = valueProvider.GetValue("Type.Id")?.AttemptedValue;
            int.TryParse(sType, out int type);
            DateTime foundation = DateTime.ParseExact(valueProvider.GetValue("Foundation").AttemptedValue, CultureFormatsModule.GetCustomDateFormat(), CultureInfo.InvariantCulture);
            string sPhId = valueProvider.GetValue("PublishingHouse.Id")?.AttemptedValue;
            int.TryParse(sPhId, out int phId);

            string authorsRaw = valueProvider.GetValue("Authors")?.AttemptedValue;
            List<AuthorViewModel> authors = authorsRaw?.Split(',').
                Select(x => { return int.TryParse(x, out int id) ? new AuthorViewModel { Id = id } : null; })
                .Where(x => x != null).ToList() ?? new List<AuthorViewModel>();

            PeriodicalViewModel periodical = new PeriodicalViewModel
            {
                Id = periodicalId,
                Name = name,
                ISSN = issn,
                Foundation = foundation,
                IsPublished = isPub,
                PublishingHouse = new PublishingHouseViewModel { Id = phId },
                Type = new PeriodicalTypeViewModel { Id = type },
            };

            return periodical;

        }
    }
}