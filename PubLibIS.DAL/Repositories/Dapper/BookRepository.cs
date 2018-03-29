using PubLibIS.DAL.Interfaces;
using PubLibIS.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using DapperExtensions;

namespace PubLibIS.DAL.Repositories.Dapper
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(DapperConnectionFactory dapperConnectionFactory)
        : base(dapperConnectionFactory) { }

        public new void  Update(Book Book)
        {
            base.Update(Book);
            ResetAuthors(Book);
        }

        //private void LoadNavigationProperties(Book b, IDbConnection db)
        //{
        //    b.PublishedBooks = db.Query<PublishedBook>($"SELECT * FROM [PublishedBooks] WHERE Book_Id = @id  ORDER BY Id", new { id = b.Id }).ToList();
        //    foreach(PublishedBook pb in b.PublishedBooks)
        //    {
        //        pb.PublishingHouse = db.QuerySingleOrDefault<PublishingHouse>($"SELECT * FROM [PublishingHouses] WHERE Id = @id ", new { id = pb.PublishingHouse_Id });
        //    }
        //    b.Authors = db.Query<AuthorInBook>($"SELECT * FROM [AuthorInBooks] WHERE Book_Id = @id  ORDER BY Id", new { id = b.Id }).ToList();
        //    foreach(AuthorInBook a in b.Authors)
        //    {
        //        a.Author = db.QuerySingleOrDefault<Author>($"SELECT * FROM [Authors] WHERE Id = @id ", new { id = a.Author_Id });
        //    }
        //}

        //private void LoadNavigationProperties(IEnumerable<Book> books, IDbConnection db)
        //{
        //    books = books.ToList();
        //    ((List<Book>)books).ForEach(p => LoadNavigationProperties(p, db));
        //}

        private void ResetAuthors(Book b)
        {
            var authorInBookRepo = new AuthorInBookRepository(dapperConnectionFactory);
            IEnumerable<AuthorInBook> authorInBookList = authorInBookRepo.GetByBookId(b.Id);
            IFieldPredicate[] fieldPredicateList =
                   authorInBookList.Select(ainb =>
                   Predicates.Field<AuthorInBook>(a => a.Id, Operator.Eq, ainb.Id))
                   .ToArray();
            IPredicateGroup predicate = Predicates.Group(GroupOperator.Or, fieldPredicateList);
            authorInBookRepo.Delete(predicate);
        }
    }
}
