using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PubLibIS.View.Util;
using PubLibIS.ViewModels;
using PubLibIS.ViewModels.Util;

namespace PubLibIS.View.Models.BindingModels
{
    public class PublishedBookModelBinder : IModelBinder
    {

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var valueProvider = bindingContext.ValueProvider;

            int? Id = (int?)valueProvider.GetValue("Id")?.ConvertTo(typeof(int));
            int BookId = (int)valueProvider.GetValue("Book_Id")?.ConvertTo(typeof(int));
            int PHId = (int)valueProvider.GetValue("PublishingHouse_Id")?.ConvertTo(typeof(int));
           DateTime DateOfP = DateTime.ParseExact(valueProvider.GetValue("DateOfPublication").AttemptedValue, CultureFormatsModule.GetCustomDateFormat(), CultureInfo.InvariantCulture);
            int Volume = (int)valueProvider.GetValue("Volume")?.ConvertTo(typeof(int));


            
            PublishedBookViewModel book = new PublishedBookViewModel
            {
                Id = Id.HasValue? Id.Value:0,
                Book = new BookViewModel { Id = BookId },
                DateOfPublication = DateOfP,
                PublishingHouse = new PublishingHouseViewModel { Id = PHId },
                Volume = Volume
            };

            return book;

        }
    }
}