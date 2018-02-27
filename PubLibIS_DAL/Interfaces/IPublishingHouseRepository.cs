using PubLibIS_DAL.Model;
using System.Collections.Generic;

namespace PubLibIS_DAL.Interfaces
{
    public interface IPublishingHouseRepository
    {
        int Create(PublishingHouse publishingHouse);
        PublishingHouse Read(int publishingHouseId);
        IEnumerable<PublishingHouse> Read();
        void Update(PublishingHouse publishingHouse);
        void Delete(int publishingHouseId);
    }
}
