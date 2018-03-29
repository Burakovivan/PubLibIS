using PubLibIS.DAL.Interfaces;
using PubLibIS.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using DapperExtensions;

namespace PubLibIS.DAL.Repositories.Dapper
{
    public class PeriodicalEditionRepository : Repository<PeriodicalEdition>, IPeriodicalEditionRepository
    {

        public PeriodicalEditionRepository(DapperConnectionFactory dapperConnectionFactory)
        : base(dapperConnectionFactory) { }

        public IEnumerable<PeriodicalEdition> GetPeriodicalEditionByPeriodicalId(int id)
        {
            IFieldPredicate predicate = Predicates.Field<PeriodicalEdition>(a => a.Periodical_Id, Operator.Ge, id);
            return GetList(predicate);
        }
    }
}
