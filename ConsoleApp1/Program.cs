using System;
using System.Collections.Generic;
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
        private static string TempJsonConnection = "tempJson";
        static void Main(string[] args)
        {
            var service = new AuthorService(new LibraryUnitOfWorkEntityFramework(LibConnection), MappingProfile.InitializeAutoMapper().CreateMapper());
            var authorIds = service.GetAuthorViewModelList().Select(a => a.Id);
            var temp = service.GetJson(authorIds);
            service = new AuthorService(new LibraryUnitOfWorkEntityFramework(TempJsonConnection), MappingProfile.InitializeAutoMapper().CreateMapper());

            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine(temp);

            service.SetJson(temp);
            Console.Read();
        }
    }
}
