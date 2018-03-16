using PubLibIS.DAL.Models;
using System;
using System.Collections.Generic;

namespace PubLibIS.DAL.Interfaces
{
    public interface IPublishingHouseRepository
    {
        int Create(PublishingHouse publishingHouse);
        PublishingHouse Get(int publishingHouseId);
        IEnumerable<PublishingHouse> Get();
        IEnumerable<PublishingHouse> Get(IEnumerable<int> IdList);
        IEnumerable<PublishingHouse> Get(int skip, int take);
        void Update(PublishingHouse publishingHouse);
        void Delete(int publishingHouseId);
    }
}
