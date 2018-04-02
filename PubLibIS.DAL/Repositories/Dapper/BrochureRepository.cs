using PubLibIS.DAL.Interfaces;
using PubLibIS.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;

namespace PubLibIS.DAL.Repositories.Dapper
{
    public class BrochureRepository : Repository<Brochure>, IBrochureRepository
    {

        public BrochureRepository(DapperConnectionFactory dapperConnectionFactory)
        : base(dapperConnectionFactory) { }

        public override void LoadNavigationProperties(Brochure entity, IDbConnection connection)
        {
            var publishingHouseRepository = new PublishingHouseRepository(dapperConnectionFactory);
            entity.PublishingHouse = entity.PublishingHouse_Id.HasValue?publishingHouseRepository.Get(entity.PublishingHouse_Id.Value):null;
        }

        

    }
}
