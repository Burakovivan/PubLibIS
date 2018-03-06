using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PubLibIS.ViewModels;

namespace PubLibIS.View.Models.BindingModels
{
    public class BookModelBinder : IModelBinder
    {

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            // Получаем поставщик значений
            var valueProvider = bindingContext.ValueProvider;

            // получаем данные по одному полю
            string sBookid = valueProvider.GetValue("Id")?.AttemptedValue;
            int.TryParse(sBookid, out int bookId);

            // получаем данные по остальным полям
            string capation = (string)valueProvider.GetValue("Capation")?.ConvertTo(typeof(string));
            string isbn = (string)valueProvider.GetValue("ISBN")?.ConvertTo(typeof(string));
            string additionalData = (string)valueProvider.GetValue("AdditionalData")?.ConvertTo(typeof(string)); ;


            string authorsRaw = valueProvider.GetValue("Authors")?.AttemptedValue;
            List<AuthorViewModel> authors = authorsRaw?.Split(',').
                Select(x => { return int.TryParse(x, out int id) ? new AuthorViewModel { Id = id } : null; })
                .Where(x => x != null).ToList() ?? new List<AuthorViewModel>();

            BookViewModel book = new BookViewModel
            {
                Id = bookId,
                Capation = capation,
                ISBN = isbn,
                Authors = authors,
                AdditionalData = additionalData
            };

            return book;

        }
    }
}