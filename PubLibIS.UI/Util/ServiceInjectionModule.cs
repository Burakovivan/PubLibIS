using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PubLibIS.BLL.Services;
using PubLibIS.BLL.Interfaces;
using Ninject;
using System.Web.Mvc;

namespace PubLibIS.UI.Util
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            kernel.Bind<AuthorService>().ToSelf();
            kernel.Bind<BookService>().ToSelf();
            kernel.Bind<BrochureService>().ToSelf();
            kernel.Bind<PeriodicalService>().ToSelf();
            kernel.Bind<PublishingHouseService>().ToSelf();
        }
    }
}