using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace PubLibIS.ViewModels.Util
{
    public static class CultureFormatsModule
    {
        public static string GetCustomDateFormat() {

            return new CultureInfo("ru-RU").DateTimeFormat.ShortDatePattern;
        }
    }
}