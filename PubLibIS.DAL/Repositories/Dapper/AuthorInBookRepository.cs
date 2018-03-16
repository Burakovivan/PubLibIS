using PubLibIS.DAL.Interfaces;
using PubLibIS.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;

namespace PubLibIS.DAL.Repositories.Dapper
{
    public class AuthorInBookRepository : IAuthorInBookRepository
    {
        private DapperConnectionFactory dapperConnectionFactory;
        private string schema;

        public AuthorInBookRepository(DapperConnectionFactory dapperConnectionFactory)
        {
            this.dapperConnectionFactory = dapperConnectionFactory;
            this.schema = dapperConnectionFactory.Schema;
        }

        public int Create(AuthorInBook AuthorInBook)
        {
            int newId = 0;
            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                newId = db.QuerySingleOrDefault<int>($"INSERT INTO [{schema}].[AuthorInBooks] VALUES " +
                    $"(@Author_Id, @Book_Id); SELECT CAST(SCOPE_IDENTITY() as int)", AuthorInBook);
            }
            return newId;
        }

        public void Delete(int AuthorInBookId)
        {
            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                db.Query($"DELETE FROM [{schema}].[AuthorInBooks] WHERE Id = @id", new { id = AuthorInBookId });
            }
        }

        public AuthorInBook Get(int AuthorInBookId)
        {
            AuthorInBook AuthorInBook = null;
            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                AuthorInBook = db.QuerySingleOrDefault<AuthorInBook>($"SELECT * FROM [{schema}].[AuthorInBooks] WHERE Id = @id", new { id = AuthorInBookId });
            }
            return AuthorInBook;
        }

        public IEnumerable<AuthorInBook> Get()
        {
            IEnumerable<AuthorInBook> AuthorInBooks = null;
            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                AuthorInBooks = db.Query<AuthorInBook>($"SELECT * FROM [{schema}].[AuthorInBooks] ORDER BY Id");
            }
            return AuthorInBooks;
        }

        public IEnumerable<AuthorInBook> Get(int skip, int take)
        {
            IEnumerable<AuthorInBook> AuthorInBooks = null;
            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {

                AuthorInBooks = db.Query<AuthorInBook>($"SELECT TOP(@take) * FROM(SELECT * FROM [{schema}].[AuthorInBooks] ORDER BY Id OFFSET @skip ROWS) as a", new { skip, take });
            }
            return AuthorInBooks;
        }

        public IEnumerable<AuthorInBook> GetByAuthorId(int authorId)
        {
            IEnumerable<AuthorInBook> AuthorInBooks = null;
            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                AuthorInBooks = db.Query<AuthorInBook>($"SELECT * FROM [{schema}].[AuthorInBooks]  WHERE Author_Id = @authorId ORDER BY Id", new { authorId });
            }
            return AuthorInBooks;
        }

        public IEnumerable<AuthorInBook> GetByAuthorId(IEnumerable<int> idList)
        {
            IEnumerable<AuthorInBook> AuthorInBooks = null;
            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                AuthorInBooks = db.Query<AuthorInBook>($"SELECT * FROM [{schema}].[AuthorInBooks] WHERE Author_Id IN @idList  ORDER BY Id", new { idList });
            }
            return AuthorInBooks;
        }

        public IEnumerable<AuthorInBook> GetByBookId(int bookId)
        {
            IEnumerable<AuthorInBook> AuthorInBooks = null;
            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                AuthorInBooks = db.Query<AuthorInBook>($"SELECT * FROM [{schema}].[AuthorInBooks]  WHERE Book_Id = @bookId ORDER BY Id", new { bookId });
            }
            return AuthorInBooks;
        }

        public IEnumerable<AuthorInBook> GetByBookId(IEnumerable<int> idList)
        {
            IEnumerable<AuthorInBook> AuthorInBooks = null;
            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                AuthorInBooks = db.Query<AuthorInBook>($"SELECT * FROM [{schema}].[AuthorInBooks] WHERE Book_Id IN @idList  ORDER BY Id", new { idList });
            }
            return AuthorInBooks;
        }

        public void Update(AuthorInBook AuthorInBook)
        {
            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                db.Query($"UPDATE [{schema}].[AuthorInBooks] SET " +
                    $"FirstName = @FirstName, SecondName = @SecondName,Patronymic = @Patronymic,DateOfBirth = @DateOfBirth,DateOfDeath = @DateOfDeath " +
                    $"WHERE Id = @Id", AuthorInBook);
            }
        }
    }
}
