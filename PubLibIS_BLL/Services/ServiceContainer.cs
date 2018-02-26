using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubLibIS_BLL.Services
{
    public class ServiceContainer
    {
        private static ServiceContainer instance;

        public static ServiceContainer GetInstance()
        {
            if (instance == null) {
                instance = new ServiceContainer();
            }
            return instance;
        }

        private ServiceContainer()
        {
            Author = new AuthorService();
        }

        public AuthorService Author { get; set; }
    }
}
