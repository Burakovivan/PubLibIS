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

        //public void LoadNavigationProperties(Periodical p, IDbConnection db)
        //{

        //    p.PeriodicalEditions = db.Query<PeriodicalEdition>($"SELECT * FROM [PeriodicalEditions] WHERE Periodical_Id = @id  ORDER BY Id", new { id = p.Id }).ToList();
        //    p.PublishingHouse = db.QuerySingleOrDefault<PublishingHouse>($"SELECT * FROM [PublishingHouses] WHERE Id = @id", new { id = p.PublishingHouse_Id });

        //}

        //public void LoadNavigationProperties(IEnumerable<Periodical> periodicals, IDbConnection db)
        //{
        //    periodicals = periodicals.ToList();
        //    ((List<Periodical>)periodicals).ForEach(p => LoadNavigationProperties(p, db));
        //}
    }
}
