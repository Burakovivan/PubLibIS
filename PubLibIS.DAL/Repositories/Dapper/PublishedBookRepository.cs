using PubLibIS.DAL.Interfaces;
using PubLibIS.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using System;
using DapperExtensions;

namespace PubLibIS.DAL.Repositories.Dapper
{
    public class PublishedBookRepository : Repository<PublishedBook>, IPublishedBookRepository
    {

        public PublishedBookRepository(DapperConnectionFactory dapperConnectionFactory)
        : base(dapperConnectionFactory) { }

        public IEnumerable<PublishedBook> GetPublishedBookByBookId(int bookId)
        {
            IFieldPredicate predicate = Predicates.Field<PublishedBook>(a => a.Book_Id, Operator.Ge, bookId);
            return GetList(predicate);
        }

        //public void LoadNavigationProperties(PublishedBook b, IDbConnection db)
        //{
        //    if(b == null)
        //    {
        //        return;
        //    }
        //    b.PublishingHouse = db.QuerySingleOrDefault<PublishingHouse>($"SELECT * FROM [{schema}].[PublishingHouses] WHERE Id = @id  ORDER BY Id", new { id = b.PublishingHouse_Id });
        //}

        //public void LoadNavigationProperties(IEnumerable<PublishedBook> periodicals, IDbConnection db)
        //{
        //    periodicals = periodicals.ToList();
        //    ((List<PublishedBook>)periodicals).ForEach(p => LoadNavigationProperties(p, db));
        //}
    }
}
