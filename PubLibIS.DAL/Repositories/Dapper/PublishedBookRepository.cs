using PubLibIS.DAL.Interfaces;
using PubLibIS.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using System;

namespace PubLibIS.DAL.Repositories.Dapper
{
    public class PublishedBookRepository : IPublishedBookRepository
    {
        private DapperConnectionFactory dapperConnectionFactory;
        private string schema;

        public PublishedBookRepository(DapperConnectionFactory dapperConnectionFactory)
        {
            this.dapperConnectionFactory = dapperConnectionFactory;
            this.schema = dapperConnectionFactory.Schema;
        }

        public int Count()
        {
            int Count = 0;

            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                Count = db.QuerySingleOrDefault<int>($"SELECT COUNT(*) FROM [{schema}].[PublishedBooks] ");
            }
            return Count;
        }

        public int Create(PublishedBook PublishedBook)
        {
            int newId = 0;
            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                newId = db.QuerySingleOrDefault<int>($"INSERT INTO [{schema}].[PublishedBooks] VALUES " +
                    $"(@Volume, @DateOfPublication, @Book_id, @PublishingHouse_id); SELECT CAST(SCOPE_IDENTITY() as int)", PublishedBook);
            }
            return newId;
        }

        public void Delete(int PublishedBookId)
        {
            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                db.Query($"DELETE FROM [{schema}].[PublishedBooks] WHERE Id = @id", new { id = PublishedBookId });
            }
        }

        public PublishedBook Get(int PublishedBookId)
        {
            PublishedBook PublishedBook = null;
            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                PublishedBook = db.QuerySingleOrDefault<PublishedBook>($"SELECT * FROM [{schema}].[PublishedBooks] WHERE Id = @id", new { id = PublishedBookId });
                LoadNavigationProperties(PublishedBook, db);
            }
            return PublishedBook;
        }

        public IEnumerable<PublishedBook> Get()
        {
            IEnumerable<PublishedBook> PublishedBooks = null;
            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                PublishedBooks = db.Query<PublishedBook>($"SELECT * FROM [{schema}].[PublishedBooks] ORDER BY Id");
            }
            return PublishedBooks;
        }

        public IEnumerable<PublishedBook> Get(IEnumerable<int> idList)
        {
            IEnumerable<PublishedBook> PublishedBooks = null;
            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                PublishedBooks = db.Query<PublishedBook>($"SELECT * FROM [{schema}].[PublishedBooks] WHERE Id IN @idList  ORDER BY Id", new { idList });
            }
            return PublishedBooks;
        }

        public IEnumerable<PublishedBook> Get(int skip, int take)
        {
            IEnumerable<PublishedBook> PublishedBooks = null;
            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {

                PublishedBooks = db.Query<PublishedBook>($"SELECT TOP(@take) * FROM(SELECT * FROM [{schema}].[PublishedBooks] ORDER BY Id OFFSET @skip ROWS) as a", new { skip, take });
            }
            return PublishedBooks;
        }

        public IEnumerable<PublishedBook> GetPublishedBookByBookId(int bookId)
        {
            IEnumerable<PublishedBook> PublishedBook = null;
            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                PublishedBook = db.Query<PublishedBook>($"SELECT * FROM [{schema}].[PublishedBooks] WHERE Book_Id = @id", new { id = bookId });
                LoadNavigationProperties(PublishedBook, db);
            }
            return PublishedBook;
        }

        public void Update(PublishedBook PublishedBook)
        {
            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                db.Query($"UPDATE [{schema}].[PublishedBooks] SET " +
                    $"Volume = @Volume, DateOfPublication = @DateOfPublication, Book_id = @Book_id, PublishingHouse_id = @PublishingHouse_id " +
                    $"WHERE Id = @Id", PublishedBook);
            }
        }

        public void LoadNavigationProperties(PublishedBook b, IDbConnection db)
        {
            b.PublishingHouse = db.QuerySingleOrDefault<PublishingHouse>($"SELECT * FROM [{schema}].[PublishingHouses] WHERE Id = @id  ORDER BY Id", new { id = b.PublishingHouse_Id });
        }

        public void LoadNavigationProperties(IEnumerable<PublishedBook> periodicals, IDbConnection db)
        {
            periodicals = periodicals.ToList();
            ((List<PublishedBook>)periodicals).ForEach(p => LoadNavigationProperties(p, db));
        }
    }
}
