using Ninject.Modules;
using PubLibIS.DAL.Interfaces;
using PubLibIS.DAL.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubLibIS.BLL.Infrastructure
{
    public class UoWInjectionModule : NinjectModule
    {
        private string connectionName;
        public UoWInjectionModule(string connectionName)
        {
            this.connectionName = connectionName;
        }
        public override void Load()
        {
            Bind<IUnitOfWork>().To<LibraryUoWEF>().WithConstructorArgument(connectionName);
        }
    }
}
