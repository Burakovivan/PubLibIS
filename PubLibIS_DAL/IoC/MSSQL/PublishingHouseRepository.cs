using PubLibIS_DAL.Interfaces;
using PubLibIS_DAL.Model;
using System.Collections.Generic;
using System.Linq;

namespace PubLibIS_DAL.IoC.MSSQL
{
    public class PublishingHouseRepository : IPublishingHouseRepository
    {
        private LibraryContext context;

        public PublishingHouseRepository(LibraryContext context)
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
            context.SaveChanges();
        }

        public PublishingHouse Read(int publishingHouseId)
        {
            return context.PublishingHouses.Find(publishingHouseId);
        }

        public IEnumerable<PublishingHouse> Read()
        {
            return context.PublishingHouses.AsEnumerable();
        }

        public void Update(PublishingHouse publishingHouse)
        {
            var current = Read(publishingHouse.Id);
            context.Entry(current).CurrentValues.SetValues(publishingHouse);
            context.SaveChanges();
        }
    }
}
