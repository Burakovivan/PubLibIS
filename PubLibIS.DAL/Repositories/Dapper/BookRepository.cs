using PubLibIS.DAL.Interfaces;
using PubLibIS.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;

namespace PubLibIS.DAL.Repositories.Dapper
{
    public class BookRepository : IBookRepository
    {
        private DapperConnectionFactory dapperConnectionFactory;
        private string schema;

        public BookRepository(DapperConnectionFactory dapperConnectionFactory)
        {
            this.dapperConnectionFactory = dapperConnectionFactory;
            this.schema = dapperConnectionFactory.Schema;
        }

        public int Count()
        {
            int Count = 0;

            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                Count = db.QuerySingleOrDefault<int>($"SELECT COUNT(*) FROM [{schema}].[Books] ");
            }
            return Count;
        }

        public int Create(Book Book)
        {
            int newId = 0;
            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                newId = db.QuerySingleOrDefault<int>($"INSERT INTO [{schema}].[Books] VALUES " +
                    $"(@ISBN, @Capation, @AdditionalData); SELECT CAST(SCOPE_IDENTITY() as int)", Book);
                Book.Id = newId;
                ResetAuthors(Book, db);
            }
            return newId;
        }

        public void Delete(int BookId)
        {
            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                db.Query($"DELETE FROM [{schema}].[Books] WHERE Id = @id", new { id = BookId });
            }
        }



        public Book Get(int BookId)
        {
            Book Book = null;
            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                Book = db.QuerySingleOrDefault<Book>($"SELECT * FROM [{schema}].[Books] WHERE Id = @id", new { id = BookId });
                LoadNavigationProperties(Book, db);
            }
            return Book;
        }

        public IEnumerable<Book> Get()
        {
            IEnumerable<Book> Books = null;
            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                Books = db.Query<Book>($"SELECT * FROM [{schema}].[Books] ORDER BY Id");
                LoadNavigationProperties(Books, db);
            }
            return Books;
        }

        public IEnumerable<Book> Get(IEnumerable<int> idList)
        {
            IEnumerable<Book> Books = null;
            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                Books = db.Query<Book>($"SELECT * FROM [{schema}].[Books] WHERE Id IN @idList  ORDER BY Id", new { idList });
                LoadNavigationProperties(Books, db);
            }
            return Books;
        }

        public IEnumerable<Book> Get(int skip, int take)
        {
            IEnumerable<Book> Books = null;
            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {

                Books = db.Query<Book>($"SELECT TOP(@take) * FROM(SELECT * FROM [{schema}].[Books] ORDER BY Id OFFSET @skip ROWS) as a", new { skip, take });
                LoadNavigationProperties(Books, db);
            }

            return Books;
        }


        public void Update(Book Book)
        {
            using (IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                db.Query($"UPDATE [{schema}].[Books] SET " +
                    $"ISBN = @ISBN, Capation = @Capation, AdditionalData = @AdditionalData " +
                    $"WHERE Id = @Id", Book);
                ResetAuthors(Book, db);
            }
        }

        private void LoadNavigationProperties(Book b, IDbConnection db)
        {

            b.PublishedBooks = db.Query<PublishedBook>($"SELECT * FROM [{schema}].[PublishedBooks] WHERE Book_Id = @id  ORDER BY Id", new { id = b.Id }).ToList();
            foreach (var pb in b.PublishedBooks)
            {
                pb.PublishingHouse = db.QuerySingleOrDefault<PublishingHouse>($"SELECT * FROM [{schema}].[PublishingHouses] WHERE Id = @id ", new { id = pb.PublishingHouse_Id });
            }
            b.Authors = db.Query<AuthorInBook>($"SELECT * FROM [{schema}].[AuthorInBooks] WHERE Book_Id = @id  ORDER BY Id", new { id = b.Id }).ToList();
            foreach (var a in b.Authors)
            {
                a.Author = db.QuerySingleOrDefault<Author>($"SELECT * FROM [{schema}].[Authors] WHERE Id = @id ", new { id = a.Author_Id });
            }
        }

        private void LoadNavigationProperties(IEnumerable<Book> books, IDbConnection db)
        {
            books = books.ToList();
            ((List<Book>)books).ForEach(p => LoadNavigationProperties(p, db));
        }

        private void ResetAuthors(Book b, IDbConnection db)
        {
            db.Query($"DELETE FROM [{schema}].[AuthorInBooks] WHERE Book_Id = @id", new { id = b.Id });
            foreach (AuthorInBook a in b.Authors)
            {
                db.Query($"INSERT INTO [{schema}].[AuthorInBooks] (Author_Id, Book_Id) VALUES " +
                      $"(@Author_Id, @Book_Id); SELECT CAST(SCOPE_IDENTITY() as int)", new { Author_Id = a.Author.Id, Book_Id = b.Id });
            }
        }
    }
}
