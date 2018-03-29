using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PubLibIS.BLL.Interfaces;
using PubLibIS.BLL.Services;
using PubLibIS.DAL.Interfaces;
using PubLibIS.DAL.UnitsOfWork;

namespace PubLibIS.CoreUI.ServiceExtensions
{
    public static class DataAccesLayerDI
    {
        public static void AddBLLDI(this IServiceCollection service)
        {
            service.AddSingleton<AuthorService>();
            service.AddSingleton<IBookService, BookService>();
            service.AddSingleton<IPublishingHouseService, PublishingHouseService>();
            service.AddSingleton<IBrochureService, BrochureService>();
            service.AddSingleton<IPeriodicalService, PeriodicalService>();
            service.AddSingleton<IServiceCreator, ServiceCreator>();
            service.AddSingleton<IUserService, UserService>();
            service.AddSingleton(provider =>
            {
                return BLL.MappingProfile.InitializeAutoMapper().CreateMapper();
            });
        }
        public static void AddDALDI(this IServiceCollection service, string connectionName)
        {
            service.AddTransient<IUnitOfWork>(provider => {
                return new LibraryUnitOfWorkDapper(connectionName);
            });
        }
    }
}
