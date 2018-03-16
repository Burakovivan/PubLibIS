using PubLibIS.DAL.Interfaces;
using PubLibIS.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using System;

namespace PubLibIS.DAL.Repositories.Dapper
{
    public class PublishingHouseRepository : IPublishingHouseRepository
    {
        private DapperConnectionFactory dapperConnectionFactory;
        private string schema;

        public PublishingHouseRepository(DapperConnectionFactory dapperConnectionFactory)
        {
            this.dapperConnectionFactory = dapperConnectionFactory;
            this.schema = dapperConnectionFactory.Schema;
        }

        public int Count()
        {
            int Count = 0;

            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                Count = db.QuerySingleOrDefault<int>($"SELECT COUNT(*) FROM [{schema}].[PublishingHouses] ");
            }
            return Count;
        }

        public int Create(PublishingHouse PublishingHouse)
        {
            int newId = 0;
            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                newId = db.QuerySingleOrDefault<int>($"INSERT INTO [{schema}].[PublishingHouses](Name, Country, City, Address, Phone,PostalCode, FoundationDate) VALUES " +
                    $"(@Name, @Country, @City, @Address, @Phone, @PostalCode, @FoundationDate); SELECT CAST(SCOPE_IDENTITY() as int)", PublishingHouse);
            }
            return newId;
        }

        public void Delete(int PublishingHouseId)
        {
            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                db.Query($"DELETE FROM [{schema}].[PublishingHouses] WHERE Id = @id", new { id = PublishingHouseId });
            }
        }



        public PublishingHouse Get(int PublishingHouseId)
        {
            PublishingHouse PublishingHouse = null;
            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                PublishingHouse = db.QuerySingleOrDefault<PublishingHouse>($"SELECT * FROM [{schema}].[PublishingHouses] WHERE Id = @id", new { id = PublishingHouseId });
            }
            return PublishingHouse;
        }

        

        public IEnumerable<PublishingHouse> Get()
        {
            IEnumerable<PublishingHouse> PublishingHouses = null;
            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                PublishingHouses = db.Query<PublishingHouse>($"SELECT * FROM [{schema}].[PublishingHouses] ORDER BY Id");
            }
            return PublishingHouses;
        }

        public IEnumerable<PublishingHouse> Get(IEnumerable<int> idList)
        {
            IEnumerable<PublishingHouse> PublishingHouses = null;
            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                PublishingHouses = db.Query<PublishingHouse>($"SELECT * FROM [{schema}].[PublishingHouses] WHERE Id IN @idList  ORDER BY Id", new { idList });
            }
            return PublishingHouses;
        }

        public IEnumerable<PublishingHouse> Get(int skip, int take)
        {
            IEnumerable<PublishingHouse> PublishingHouses = null;
            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {

                PublishingHouses = db.Query<PublishingHouse>($"SELECT TOP(@take) * FROM(SELECT * FROM [{schema}].[PublishingHouses] ORDER BY Id OFFSET @skip ROWS) as a", new { skip, take });
            }
            return PublishingHouses;
        }

        public void Update(PublishingHouse PublishingHouse)
        {
            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                db.Query($"UPDATE [{schema}].[PublishingHouses] SET " +
                    $"Name = @Name, Country = @Country, City = @City, Address = @Address, Phone = @Phone, PostalCode = @PostalCode, FoundationDate = @FoundationDate " +
                    $"WHERE Id = @Id", PublishingHouse);
            }
        }
    }
}
