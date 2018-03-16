using AutoMapper;
using Ninject.Modules;
using PubLibIS.DAL.Interfaces;
using PubLibIS.DAL.UnitsOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubLibIS.BLL.Infrastructure
{
    public class UnitOfWWorkInjectionModule : NinjectModule
    {
        private string connectionName;
        public UnitOfWWorkInjectionModule(string connectionName)
        {
            this.connectionName = connectionName;
        }
        public override void Load()
        {
            Bind<IUnitOfWork>().To<LibraryUnitOfWorkEntityFramework>()
            //Bind<IUnitOfWork>().To<LibraryUnitOfWorkDapper>()
                .WithConstructorArgument(connectionName);
            Bind<IMapper>().ToConstant(MappingProfile.InitializeAutoMapper().CreateMapper());
        }
    }
}
