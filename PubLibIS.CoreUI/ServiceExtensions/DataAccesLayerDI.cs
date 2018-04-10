using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
      service.AddSingleton<BookService>();
      service.AddSingleton<PublishingHouseService>();
      service.AddSingleton<BrochureService>();
      service.AddSingleton<PeriodicalService>();
      service.AddSingleton<BackupFileService>();
      service.AddSingleton<ServiceCreator>();
      service.AddSingleton<UserService>();
      service.AddSingleton(provider =>
      {
        return BLL.MappingProfile.InitializeAutoMapper().CreateMapper();
      });
    }
    public static void AddDALDI(this IServiceCollection service, string connectionName)
    {
      service.AddTransient<IUnitOfWork>(provider =>
      {
        return new LibraryUnitOfWorkEntityFramework(connectionName);
      });
    }
  }
}
