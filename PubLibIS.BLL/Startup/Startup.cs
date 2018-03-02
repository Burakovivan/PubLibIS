using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

[assembly: PreApplicationStartMethod(typeof(PubLibIS.BLL.Startup), "Start")]

namespace PubLibIS.BLL
{
    public class Startup
    {
        public static void Start()
        {
            MapperConfiguration.Initialize();
        }
    }

}
