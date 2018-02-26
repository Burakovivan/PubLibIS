using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

[assembly: PreApplicationStartMethod(typeof(PubLibIS_BLL.Startup), "Start")]

namespace PubLibIS_BLL
{
    public class Startup
    {
        public static void Start()
        {
            MapperConfiguration.Initialize();
        }
    }

}
