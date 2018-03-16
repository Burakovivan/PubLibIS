using PubLibIS.DAL.Interfaces;
using PubLibIS.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;

namespace PubLibIS.DAL.Repositories.Dapper
{
    public class PeriodicalEditionRepository : IPeriodicalEditionRepository
    {
        private DapperConnectionFactory dapperConnectionFactory;
        private string schema;

        public PeriodicalEditionRepository(DapperConnectionFactory dapperConnectionFactory)
        {
            this.dapperConnectionFactory = dapperConnectionFactory;
            this.schema = dapperConnectionFactory.Schema;
        }

        public int Count()
        {
            int Count = 0;

            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                Count = db.QuerySingleOrDefault<int>($"SELECT COUNT(*) FROM [{schema}].[PeriodicalEditions] ");
            }
            return Count;
        }

        public int Create(PeriodicalEdition PeriodicalEdition)
        {
            int newId = 0;
            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                newId = db.QuerySingleOrDefault<int>($"INSERT INTO [{schema}].[PeriodicalEditions] VALUES " +
                    $"(@ReleaseNumber, @ReleaseDate, @Circulation, @Periodical_Id); SELECT CAST(SCOPE_IDENTITY() as int)", PeriodicalEdition);
            }
            return newId;
        }

        public void Delete(int PeriodicalEditionId)
        {
            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                db.Query($"DELETE FROM [{schema}].[PeriodicalEditions] WHERE Id = @id", new { id = PeriodicalEditionId });
            }
        }



        public PeriodicalEdition Get(int PeriodicalEditionId)
        {
            PeriodicalEdition PeriodicalEdition = null;
            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                PeriodicalEdition = db.QuerySingleOrDefault<PeriodicalEdition>($"SELECT * FROM [{schema}].[PeriodicalEditions] WHERE Id = @id", new { id = PeriodicalEditionId });
            }
            return PeriodicalEdition;
        }

        public IEnumerable<PeriodicalEdition> Get()
        {
            IEnumerable<PeriodicalEdition> PeriodicalEditions = null;
            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                PeriodicalEditions = db.Query<PeriodicalEdition>($"SELECT * FROM [{schema}].[PeriodicalEditions] ORDER BY Id");
            }
            return PeriodicalEditions;
        }

        public IEnumerable<PeriodicalEdition> Get(IEnumerable<int> idList)
        {
            IEnumerable<PeriodicalEdition> PeriodicalEditions = null;
            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                PeriodicalEditions = db.Query<PeriodicalEdition>($"SELECT * FROM [{schema}].[PeriodicalEditions] WHERE Id IN @idList  ORDER BY Id", new { idList });
            }
            return PeriodicalEditions;
        }

        public IEnumerable<PeriodicalEdition> Get(int skip, int take)
        {
            IEnumerable<PeriodicalEdition> PeriodicalEditions = null;
            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {

                PeriodicalEditions = db.Query<PeriodicalEdition>($"SELECT TOP(@take) * FROM(SELECT * FROM [{schema}].[PeriodicalEditions] ORDER BY Id OFFSET @skip ROWS) as a", new { skip, take });
            }
            return PeriodicalEditions;
        }

        public IEnumerable<PeriodicalEdition> GetPeriodicalEditionByPeriodicalId(int id)
        {
            IEnumerable<PeriodicalEdition> PeriodicalEdtionList = null;
            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                PeriodicalEdtionList = db.Query<PeriodicalEdition>($"SELECT * FROM [{schema}].[PeriodicalEditions]  WHERE Periodical_Id = @id ORDER BY Id", new { id });
            }
            return PeriodicalEdtionList;
        }

        public void Update(PeriodicalEdition PeriodicalEdition)
        {
            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                db.Query($"UPDATE [{schema}].[PeriodicalEditions] SET " +
                    $"ReleaseNumber = @ReleaseNumber, ReleaseDate = @ReleaseDate, Ciculation = @Ciculation, Periodical_Id = @Periodical_Id" +
                    $"WHERE Id = @Id", PeriodicalEdition);
            }
        }
    }
}
