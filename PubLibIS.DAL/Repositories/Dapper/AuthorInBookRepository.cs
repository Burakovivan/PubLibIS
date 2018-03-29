using PubLibIS.DAL.Interfaces;
using PubLibIS.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using DapperExtensions;
using System.Linq.Expressions;

namespace PubLibIS.DAL.Repositories.Dapper
{
    public class AuthorInBookRepository : Repository<AuthorInBook>, IAuthorInBookRepository
    {

        public AuthorInBookRepository(DapperConnectionFactory dapperConnectionFactory)
        : base(dapperConnectionFactory) { }

        public IEnumerable<AuthorInBook> GetByAuthorId(int authorId)
        {
            IFieldPredicate predicate = Predicates.Field<AuthorInBook>(a => a.Author_Id, Operator.Ge, authorId);
            return GetList(predicate);
        }

        public IEnumerable<AuthorInBook> GetByAuthorIdList(IEnumerable<int> idList)
        {
            IFieldPredicate[] fieldPredicateList =
                    idList.Select(id =>
                    Predicates.Field<AuthorInBook>(a => a.Author_Id, Operator.Eq, id))
                    .ToArray();
            IPredicateGroup predicate = Predicates.Group(GroupOperator.Or, fieldPredicateList);
            return GetList(predicate);
        }

        public IEnumerable<AuthorInBook> GetByBookId(int bookId)
        {
            IFieldPredicate predicate = Predicates.Field<AuthorInBook>(a => a.Book_Id, Operator.Ge, bookId);
            return GetList(predicate);
        }

        public IEnumerable<AuthorInBook> GetByBookIdList(IEnumerable<int> idList)
        {
            IFieldPredicate[] fieldPredicateList =
                    idList.Select(id =>
                    Predicates.Field<AuthorInBook>(a => a.Book_Id, Operator.Eq, id))
                    .ToArray();
            IPredicateGroup predicate = Predicates.Group(GroupOperator.Or, fieldPredicateList);
            return GetList(predicate);
        }
    }
}
