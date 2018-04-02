using PubLibIS.DAL.Interfaces;
using PubLibIS.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using System;

namespace PubLibIS.DAL.Repositories.Dapper
{
    public class PublishedBookRepository : Repository<PublishedBook>, IPublishedBookRepository
    {

        public PublishedBookRepository(DapperConnectionFactory dapperConnectionFactory)
        : base(dapperConnectionFactory) { }

        public IEnumerable<PublishedBook> GetPublishedBookByBookId(int bookId)
        {
            return GetList().Where(pb => pb.Id == bookId);
        }

        public override void LoadNavigationProperties(PublishedBook b, IDbConnection db)
        {
            if(b?.PublishingHouse == null)
            {
                var publishingHouseRepository = new PublishingHouseRepository(dapperConnectionFactory);
                b.PublishingHouse = b.PublishingHouse_Id.HasValue ? publishingHouseRepository.Get(b.PublishingHouse_Id.Value) : null;
            }

        }

       
    }
}
