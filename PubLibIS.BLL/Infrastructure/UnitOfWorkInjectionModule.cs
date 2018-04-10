using AutoMapper;
using Ninject.Modules;
using PubLibIS.DAL.Interfaces;
using PubLibIS.DAL.UnitsOfWork;

namespace PubLibIS.BLL.Infrastructure
{
    public class UnitOfWorkInjectionModule : NinjectModule
    {
        private string connectionName;
        public UnitOfWorkInjectionModule(string connectionName)
        {
            this.connectionName = connectionName;
        }
        public override void Load()
        {
            Bind<IUnitOfWork>().To<LibraryUnitOfWorkEntityFramework>()
                .WithConstructorArgument(connectionName);
            Bind<IMapper>().ToConstant(MappingProfile.InitializeAutoMapper().CreateMapper());
        }
    }
}
