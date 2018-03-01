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
            PublishingHouse = new PublishingHouseService();
            Book = new BookService();
            PublishedBook = new PublishedBookService();
            Periodical = new PeriodicalService();
        }

        public AuthorService Author { get; set; }
        public PublishingHouseService PublishingHouse { get; set; }
        public BookService Book { get; set; }
        public PublishedBookService PublishedBook { get; set; }
        public PeriodicalService Periodical { get; set; }
    }
}
