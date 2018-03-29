using PubLibIS.DAL.Models;
using System;
using System.Collections.Generic;

namespace PubLibIS.DAL.Interfaces
{
    public interface IPublishingHouseRepository
    {
        int Create(PublishingHouse publishingHouse);
        PublishingHouse Get(int publishingHouseId);
        IEnumerable<PublishingHouse> GetList();
        IEnumerable<PublishingHouse> GetList(IEnumerable<int> IdList);
        IEnumerable<PublishingHouse> GetList(int skip, int take);
        void Update(PublishingHouse publishingHouse);
        void Delete(int publishingHouseId);
    }
}
