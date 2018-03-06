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
        static void Main(string[] args)
        {
            var service = new AuthorService(new LibraryUnitOfWorkEntityFramework(""), MappingProfile.InitializeAutoMapper().CreateMapper());
            var authorIds = service.GetAuthorViewModelList().Select(a => a.Id);
            var temp = service.GetAuthorJson(authorIds);
            Console.WriteLine(temp);
        }
    }
}
