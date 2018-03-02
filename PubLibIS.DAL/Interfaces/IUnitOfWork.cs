using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubLibIS.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IArticleRepository Articles { get; }
        IAuthorRepository Authors { get; }
        IBookRepository Books { get; }
        IBrochureRepository Brochures { get; }
        IPeriodicalRepository Periodicals { get; }
        IPeriodicalEditionRepository PeriodicalEditions { get; }
        IPublishingHouseRepository PublishingHouses { get; }
        IPublishedBookRepository PublishedBooks { get; }
        void Save();
    }
}
