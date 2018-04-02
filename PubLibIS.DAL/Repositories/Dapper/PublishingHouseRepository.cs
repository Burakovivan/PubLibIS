using PubLibIS.DAL.Interfaces;
using PubLibIS.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using System;

namespace PubLibIS.DAL.Repositories.Dapper
{
    public class PublishingHouseRepository : Repository<PublishingHouse>, IPublishingHouseRepository
    {
        public PublishingHouseRepository(DapperConnectionFactory dapperConnectionFactory)
        : base(dapperConnectionFactory) { }

        public override void LoadNavigationProperties(PublishingHouse entity, IDbConnection connection)
        {
           
        }


    }
}
