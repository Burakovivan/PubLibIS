using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PubLibIS.ViewModels;

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
            DateTime DateOfP = DateTime.ParseExact(valueProvider.GetValue("DateOfPublication").AttemptedValue,"dd.MM.yyyy", CultureInfo.InvariantCulture);
            int Volume = (int)valueProvider.GetValue("Volume")?.ConvertTo(typeof(int));


            string authorsRaw = valueProvider.GetValue("Authors")?.AttemptedValue;
            List<AuthorViewModel> authors = authorsRaw?.Split(',').
                Select(x => { return int.TryParse(x, out int id) ? new AuthorViewModel { Id = id } : null; })
                .Where(x => x != null).ToList() ?? new List<AuthorViewModel>();

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