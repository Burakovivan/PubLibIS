using PubLibIS.ViewModels.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PubLibIS.ViewModels.Attributes
{
    public class CustomDataDisplayFormatAttribute: DisplayFormatAttribute
    {
        public CustomDataDisplayFormatAttribute() : base()
        {
            DataFormatString = $"{{0:{CultureFormatsModule.GetCustomDateFormat()}}}";
            ApplyFormatInEditMode = true;
        }
    }
}