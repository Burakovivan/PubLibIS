using PubLibIS.DAL.Interfaces;
using PubLibIS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PubLibIS.DAL.Repositories.EntityFramework
{
    public class PublishingHouseRepository : IPublishingHouseRepository
    {
        private LibraryEntityFrameworkContext context;

        public PublishingHouseRepository(LibraryEntityFrameworkContext context)
        {
            this.context = context;
        }

        public int Create(PublishingHouse publishingHouse)
        {
            context.PublishingHouses.Add(publishingHouse);
            context.SaveChanges();
            return publishingHouse.Id;
        }

        public void Delete(int publishingHouseId)
        {
            var publishingHouse = Get(publishingHouseId);
            context.PublishingHouses.Remove(publishingHouse);
        }

        public PublishingHouse Get(int publishingHouseId)
        {
            return context.PublishingHouses.Find(publishingHouseId);
        }

        public IEnumerable<PublishingHouse> Get()
        {
            return context.PublishingHouses.AsEnumerable();
        }

        public IEnumerable<PublishingHouse> Get(int skip, int take)
        {
            return context.PublishingHouses.Skip(skip).Take(take);
        }

        public IEnumerable<PublishingHouse> Get(IEnumerable<int> IdList)
        {
            return context.PublishingHouses.Where(ph => IdList.Contains(ph.Id));
        }

        public void Update(PublishingHouse publishingHouse)
        {
            var current = Get(publishingHouse.Id);
            context.Entry(current).CurrentValues.SetValues(publishingHouse);
        }
    }
}
