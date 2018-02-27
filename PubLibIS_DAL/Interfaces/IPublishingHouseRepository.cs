using PubLibIS_DAL.Model;
using System.Collections.Generic;

namespace PubLibIS_DAL.Interfaces
{
    public interface IPublishingHouseRepository
    {
        int Create(PublishingHouse publishingHouse);
        PublishingHouse Read(int publishingHouseId);
        IEnumerable<PublishingHouse> Read();
        IEnumerable<PublishingHouse> Read(int skip, int take);
        void Update(PublishingHouse publishingHouse);
        void Delete(int publishingHouseId);
    }
}
