using Ninject;
using Ninject.Modules;
using PubLibIS.BLL.Infrastructure;
using PubLibIS.UI.Models.BindingModels;
using PubLibIS.UI.Util;
using PubLibIS.ViewModels.Util;
using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PubLibIS.UI
{
    public class MvcApplication : System.Web.HttpApplication
    {

        protected void Application_Start()
        {

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ModelBinders.Binders.Add(typeof(ViewModels.BookViewModel), new BookModelBinder());
            ModelBinders.Binders.Add(typeof(ViewModels.PublishedBookViewModel), new PublishedBookModelBinder());
           // ModelBinders.Binders.Add(typeof(ViewModels.PeriodicalViewModel), new PeriodicalModelBinder());
            var binder = new DateTimeModelBinder(CultureFormatsModule.GetCustomDateFormat());
            ModelBinders.Binders.Add(typeof(DateTime), binder);
            ModelBinders.Binders.Add(typeof(DateTime?), binder);
            DependencyResolverSetter.Inject();
         

        }
    }
}
