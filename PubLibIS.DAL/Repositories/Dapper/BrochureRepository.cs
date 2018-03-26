using PubLibIS.DAL.Interfaces;
using PubLibIS.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;

namespace PubLibIS.DAL.Repositories.Dapper
{
    public class BrochureRepository : IBrochureRepository
    {
        private DapperConnectionFactory dapperConnectionFactory;
        private string schema;

        public BrochureRepository(DapperConnectionFactory dapperConnectionFactory)
        {
            this.dapperConnectionFactory = dapperConnectionFactory;
            this.schema = dapperConnectionFactory.Schema;
        }

        public int Count()
        {
            int Count = 0;

            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                Count = db.QuerySingleOrDefault<int>($"SELECT COUNT(*) FROM [{schema}].[Brochures] ");
            }
            return Count;
        }

        public int Create(Brochure Brochure)
        {
            int newId = 0;
            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                newId = db.QuerySingleOrDefault<int>($"INSERT INTO [{schema}].[Brochures] VALUES " +
                    $"@Capation, @Volume, @ReleaseDate, @Circulation, @PublishingHouse_Id); SELECT CAST(SCOPE_IDENTITY() as int)", Brochure);
            }
            return newId;
        }

        public void Delete(int BrochureId)
        {
            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                db.Query($"DELETE FROM [{schema}].[Brochures] WHERE Id = @id", new { id = BrochureId });
            }
        }



        public Brochure Get(int BrochureId)
        {
            Brochure Brochure = null;
            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                Brochure = db.QuerySingleOrDefault<Brochure>($"SELECT * FROM [{schema}].[Brochures] WHERE Id = @id", new { id = BrochureId });
                LoadNavigationProperties(Brochure, db);
            }
            return Brochure;
        }

        public IEnumerable<Brochure> Get()
        {
            IEnumerable<Brochure> Brochures = null;
            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                Brochures = db.Query<Brochure>($"SELECT * FROM [{schema}].[Brochures] ORDER BY Id");
                LoadNavigationProperties(Brochures, db);
            }
            return Brochures;
        }

        public IEnumerable<Brochure> Get(IEnumerable<int> idList)
        {
            IEnumerable<Brochure> Brochures = null;
            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                Brochures = db.Query<Brochure>($"SELECT * FROM [{schema}].[Brochures] WHERE Id IN @idList  ORDER BY Id", new { idList });
                LoadNavigationProperties(Brochures, db);
            }
            return Brochures;
        }

        public IEnumerable<Brochure> Get(int skip, int take)
        {
            IEnumerable<Brochure> Brochures = null;
            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                Brochures = db.Query<Brochure>($"SELECT TOP(@take) * FROM(SELECT * FROM [{schema}].[Brochures] ORDER BY Id OFFSET @skip ROWS) as a", new { skip, take });
                LoadNavigationProperties(Brochures, db);
            }
            return Brochures;
        }


        public void Update(Brochure Brochure)
        {
            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                db.Query($"UPDATE [{schema}].[Brochures] SET " +
                    $"Volume = @Volume, Capation = @Capation, ReleaseDate = @ReleaseDate, Circulation = @Circulation, PublishingHouse_Id = @PublishingHouse_Id " +
                    $"WHERE Id = @Id", Brochure);
            }
        }

        public void LoadNavigationProperties(Brochure brochure, IDbConnection db)
        {
            if (brochure.PublishingHouse_Id != null)
            {
                brochure.PublishingHouse = db.QuerySingleOrDefault<PublishingHouse>($"SELECT * FROM [{schema}].[PublishingHouses] WHERE Id = @id", new { id = brochure.PublishingHouse_Id });
            }
        }

        public void LoadNavigationProperties(IEnumerable<Brochure> brochures, IDbConnection db)
        {
            brochures = brochures.ToList();
            ((List<Brochure>)brochures).ForEach(p => LoadNavigationProperties(p, db));
        }
    }
}
