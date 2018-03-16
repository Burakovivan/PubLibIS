using PubLibIS.DAL.Interfaces;
using PubLibIS.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;

namespace PubLibIS.DAL.Repositories.Dapper
{
    public class AuthorRepository : IAuthorRepository
    {
        private DapperConnectionFactory dapperConnectionFactory;
        private string schema;

        public AuthorRepository(DapperConnectionFactory dapperConnectionFactory)
        {
            this.dapperConnectionFactory = dapperConnectionFactory;
            this.schema = dapperConnectionFactory.Schema;
        }

        public int Create(Author author)
        {
            int newId = 0;
            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                newId = db.QuerySingleOrDefault<int>($"INSERT INTO [{schema}].[Authors] VALUES " +
                    $"(@FirstName, @SecondName, @Patronymic, @DateOfBirth, @DateOfDeath); SELECT CAST(SCOPE_IDENTITY() as int)", author);
                
            }
            return newId;
        }

        public void Delete(int authorId)
        {
            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                db.Query($"DELETE FROM [{schema}].[Authors] WHERE Id = @id", new { id = authorId });
            }
        }

        public Author Get(int authorId)
        {
            Author author = null;
            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                author = db.QuerySingleOrDefault<Author>($"SELECT * FROM [{schema}].[Authors] WHERE Id = @id", new { id = authorId });
            }
            return author;
        }

        public IEnumerable<Author> Get()
        {
            IEnumerable<Author> authors = null;
            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                authors = db.Query<Author>($"SELECT * FROM [{schema}].[Authors] ORDER BY Id");
            }
            return authors;
        }

        public IEnumerable<Author> Get(IEnumerable<int> idList)
        {
            IEnumerable<Author> authors = null;
            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                authors = db.Query<Author>($"SELECT * FROM [{schema}].[Authors] WHERE Id IN @idList  ORDER BY Id", new { idList});
            }
            return authors;
        }

        public IEnumerable<Author> Get(int skip, int take)
        {
            IEnumerable<Author> authors = null;
            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
               
                authors = db.Query<Author>($"SELECT TOP(@take) * FROM(SELECT * FROM [{schema}].[Authors] ORDER BY Id OFFSET @skip ROWS) as a", new { skip,take});
            }
            return authors;
        }

        public void Update(Author author)
        {
            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                db.Query($"UPDATE [{schema}].[Authors] SET " +
                    $"FirstName = @FirstName, SecondName = @SecondName,Patronymic = @Patronymic,DateOfBirth = @DateOfBirth,DateOfDeath = @DateOfDeath " +
                    $"WHERE Id = @Id", author);
            }
        }
    }
}
