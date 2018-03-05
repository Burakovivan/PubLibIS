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
            var publishingHouse = Read(publishingHouseId);
            context.PublishingHouses.Remove(publishingHouse);
        }

        public PublishingHouse Read(int publishingHouseId)
        {
            return context.PublishingHouses.Find(publishingHouseId);
        }

        public IEnumerable<PublishingHouse> Read()
        {
            return context.PublishingHouses.AsEnumerable();
        }

        public IEnumerable<PublishingHouse> Read(int skip, int take)
        {
            return context.PublishingHouses.Skip(skip).Take(take);
        }

        public IEnumerable<T> Select<T>(Func<PublishingHouse, T> predicate)
        {
            return context.PublishingHouses.Select(predicate);
        }

        public void Update(PublishingHouse publishingHouse)
        {
            var current = Read(publishingHouse.Id);
            context.Entry(current).CurrentValues.SetValues(publishingHouse);
        }
    }
}
