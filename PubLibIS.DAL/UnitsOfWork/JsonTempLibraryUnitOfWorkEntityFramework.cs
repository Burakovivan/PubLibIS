using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubLibIS.DAL.UnitsOfWork
{
    public class JsonTempLibraryUnitOfWorkEntityFramework : LibraryUnitOfWorkEntityFramework
    {
        public JsonTempLibraryUnitOfWorkEntityFramework() : base("tempJson") { }
    }
}
