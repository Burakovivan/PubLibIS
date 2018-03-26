using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PubLibIS.ViewModels;

namespace PubLibIS.UI.Models.BindingModels
{
    public class DateTimeModelBinder : DefaultModelBinder
    {
        private string _customFormat;

        public DateTimeModelBinder(string customFormat)
        {
            _customFormat = customFormat;
        }

        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (DateTime.TryParseExact(value.AttemptedValue, _customFormat, CultureInfo.InvariantCulture,DateTimeStyles.None, out DateTime result))
                return result;
            else
                return new DateTime?();
        }
    }
}