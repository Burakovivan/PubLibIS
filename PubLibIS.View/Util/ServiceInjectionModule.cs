using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PubLibIS.BLL.Services;
using PubLibIS.BLL.Interfaces;
using Ninject;
using System.Web.Mvc;

namespace PubLibIS.View.Util
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
            kernel.Bind<IAuthorService>().To<AuthorService>();
            kernel.Bind<IBookService>().To<BookService>();
            kernel.Bind<IBrochureService>().To<BrochureService>();
            kernel.Bind<IPeriodicalService>().To<PeriodicalService>();
            kernel.Bind<IPublishingHouseService>().To<PublishingHouseService>();
        }
    }
}