using Ninject;
using Ninject.Modules;
using PubLibIS.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PubLibIS.View.Util
{
    public static class DependencyResolverInjector
    {
        public static void Inject()
        {
            var connection = ConnectionStringResolver.CurrentConnectionString;
            NinjectModule uowInj = new UnitOfWWorkInjectionModule(connection);
            var kernel = new StandardKernel(uowInj);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
}