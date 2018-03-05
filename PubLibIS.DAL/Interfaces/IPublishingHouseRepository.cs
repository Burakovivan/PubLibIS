using PubLibIS.DAL.Models;
using System;
using System.Collections.Generic;

namespace PubLibIS.DAL.Interfaces
{
    public interface IPublishingHouseRepository
    {
        int Create(PublishingHouse publishingHouse);
        PublishingHouse Read(int publishingHouseId);
        IEnumerable<PublishingHouse> Read();
        IEnumerable<PublishingHouse> Read(int skip, int take);
        IEnumerable<T> Select<T>(Func<PublishingHouse, T> predicate);
        void Update(PublishingHouse publishingHouse);
        void Delete(int publishingHouseId);
    }
}
