using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PubLibIS.BLL;
using PubLibIS.BLL.Services;
using PubLibIS.DAL.UnitsOfWork;

namespace ConsoleApp1
{
    class Program
    {
        private static string LibConnection = "LibConnection";
        //private static string TempJsonConnection = "tempJson";
        static void Main(string[] args)
        {
            //var service = new PublishingHouseService(new LibraryUnitOfWorkEntityFramework(LibConnection), MappingProfile.InitializeAutoMapper().CreateMapper());
            //var authorIds = service.GetPublishingHouseViewModelSlimList().Select(a => a.Id);
            //var temp = service.GetJson(authorIds);

            //Console.OutputEncoding = Encoding.UTF8;
            //Console.WriteLine(temp);

            //service.SetJson(temp);
            //Console.Read();

            var factory = new PubLibIS.DAL.DapperConnectionFactory(ConfigurationManager.ConnectionStrings[LibConnection].ConnectionString);
            var repository = new PubLibIS.DAL.Repositories.Dapper.AuthorRepository(factory);
            var a = repository.Get(7);
            a.FirstName += "update";
            a.SecondName += "update";
            a.Patronymic += "update";
            a.DateOfBirth = a.DateOfBirth.Value.AddYears(100);
            a.DateOfDeath= a.DateOfDeath.Value.AddYears(100);
            repository.Update(a);
        }
    }
}
