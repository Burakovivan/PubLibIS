using PubLibIS.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using PubLibIS.Domain.Entities;

namespace PubLibIS.DAL.Repositories.Dapper
{
    public class PeriodicalEditionRepository : Repository<PeriodicalEdition>, IPeriodicalEditionRepository
    {

        public PeriodicalEditionRepository(DapperConnectionFactory dapperConnectionFactory)
        : base(dapperConnectionFactory) { }

        public IEnumerable<PeriodicalEdition> GetPeriodicalEditionByPeriodicalId(int id)
        {
            return GetList().Where(p => p.Id == id);
        }

    }
}
