using PubLibIS.DAL.Interfaces;
using PubLibIS.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;

namespace PubLibIS.DAL.Repositories.Dapper
{
    public class PeriodicalRepository : Repository<Periodical>, IPeriodicalRepository
    {

        public PeriodicalRepository(DapperConnectionFactory dapperConnectionFactory)
        : base(dapperConnectionFactory) { }

        public override void LoadNavigationProperties(Periodical entity, IDbConnection connection)
        {
            var publishingHouseRepository = new PublishingHouseRepository(dapperConnectionFactory);
            publishingHouseRepository.SuperReference = typeof(Periodical);
            entity.PublishingHouse = entity.PublishingHouse_Id.HasValue ? publishingHouseRepository.Get(entity.PublishingHouse_Id.Value) : null;
            var PeriodicalEditionRepository = new PeriodicalEditionRepository(dapperConnectionFactory);
            entity.PeriodicalEditions = PeriodicalEditionRepository.GetPeriodicalEditionByPeriodicalId(entity.Id).ToArray();
        }

       

     
    }
}
