using Ninject;
using Ninject.Modules;
using PubLibIS.BLL.Infrastructure;
using System.Web.Mvc;

namespace PubLibIS.UI.Util
{
    public static class DependencyResolverSetter
    {
        public static void Inject()
        {
            var connection = ConnectionStringResolver.CurrentConnectionString;
            NinjectModule uowInj = new UnitOfWorkInjectionModule(connection);
            var kernel = new StandardKernel(uowInj);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
}