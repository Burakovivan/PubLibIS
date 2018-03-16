using PubLibIS.DAL.Interfaces;
using PubLibIS.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;

namespace PubLibIS.DAL.Repositories.Dapper
{
    public class PeriodicalRepository : IPeriodicalRepository
    {
        private DapperConnectionFactory dapperConnectionFactory;
        private string schema;

        public PeriodicalRepository(DapperConnectionFactory dapperConnectionFactory)
        {
            this.dapperConnectionFactory = dapperConnectionFactory;
            this.schema = dapperConnectionFactory.Schema;
        }

        public int Count()
        {
            int Count = 0;

            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                Count = db.QuerySingleOrDefault<int>($"SELECT COUNT(*) FROM [{schema}].[Periodicals] ");
            }
            return Count;
        }

        public int Create(Periodical Periodical)
        {
            int newId = 0;
            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                newId = db.QuerySingleOrDefault<int>($"INSERT INTO [{schema}].[Periodicals] VALUES " +
                    $"@ISSN, @Type, @Name, @Foundation, @IsPublished, @IsPublished, @PublishingHouse_Id); SELECT CAST(SCOPE_IDENTITY() as int)", Periodical);
            }
            return newId;
        }

        public void Delete(int PeriodicalId)
        {
            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                db.Query($"DELETE FROM [{schema}].[Periodicals] WHERE Id = @id", new { id = PeriodicalId });
            }
        }



        public Periodical Get(int PeriodicalId)
        {
            Periodical Periodical = null;
            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                Periodical = db.QuerySingleOrDefault<Periodical>($"SELECT * FROM [{schema}].[Periodicals] WHERE Id = @id", new { id = PeriodicalId });
                LoadNavigationProperties(Periodical, db);
            }
            return Periodical;
        }

        public IEnumerable<Periodical> Get()
        {
            IEnumerable<Periodical> Periodicals = null;
            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                Periodicals = db.Query<Periodical>($"SELECT * FROM [{schema}].[Periodicals] ORDER BY Id");
            }
            return Periodicals;
        }

        public IEnumerable<Periodical> Get(IEnumerable<int> idList)
        {
            IEnumerable<Periodical> Periodicals = null;
            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                Periodicals = db.Query<Periodical>($"SELECT * FROM [{schema}].[Periodicals] WHERE Id IN @idList  ORDER BY Id", new { idList });
            }
            return Periodicals;
        }

        public IEnumerable<Periodical> Get(int skip, int take)
        {
            IEnumerable<Periodical> Periodicals = null;
            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {

                Periodicals = db.Query<Periodical>($"SELECT TOP(@take) * FROM(SELECT * FROM [{schema}].[Periodicals] ORDER BY Id OFFSET @skip ROWS) as a", new { skip, take });
                LoadNavigationProperties(Periodicals, db);
            }
            return Periodicals;
        }


        public void Update(Periodical Periodical)
        {
            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                db.Query($"UPDATE [{schema}].[Periodicals] SET " +
                    $"ISSN = @ISSN, Type = @Type, Name = @Name, Foundation = @Foundation,IsPublished = @IsPublished, PublishingHouse_Id = @PublishingHouse_Id " +
                    $"WHERE Id = @Id", Periodical);
            }
        }

        public void LoadNavigationProperties(Periodical p, IDbConnection db)
        {

            p.PeriodicalEditions = db.Query<PeriodicalEdition>($"SELECT * FROM [{schema}].[PeriodicalEditions] WHERE Periodical_Id = @id  ORDER BY Id", new { id = p.Id }).ToList();
            p.PublishingHouse = db.QuerySingleOrDefault<PublishingHouse>($"SELECT * FROM [{schema}].[PublishingHouses] WHERE Id = @id", new { id = p.PublishingHouse_Id });
            
        }

        public void LoadNavigationProperties(IEnumerable<Periodical> periodicals, IDbConnection db)
        {
            periodicals = periodicals.ToList();
            ((List<Periodical>)periodicals).ForEach(p => LoadNavigationProperties(p, db));
        }
    }
}
