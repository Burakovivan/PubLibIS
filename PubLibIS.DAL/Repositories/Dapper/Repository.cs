using PubLibIS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions;
using System.Data;


namespace PubLibIS.DAL.Repositories.Dapper
{
    public class Repository<TEntity> where TEntity : BaseEntity
    {
        protected DapperConnectionFactory dapperConnectionFactory;

        public Repository(DapperConnectionFactory dapperConnectionFactory)
        {
            this.dapperConnectionFactory = dapperConnectionFactory;
        }

        public TEntity Get(int id)
        {
            using(IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                return db.Get<TEntity>(id);
            }
        }
        public IEnumerable<TEntity> GetList()
        {
            return GetList((IPredicate)default);
        }

        protected IEnumerable<TEntity> GetList(IPredicate predicate = null)
        {
            using(IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                return db.GetList<TEntity>(predicate).ToList();
            }
        }

        public IEnumerable<TEntity> GetList(IEnumerable<int> idList)
        {
            IFieldPredicate[] fieldPredicateList =
                   idList.Select(id =>
                   Predicates.Field<TEntity>(a => a.Id, Operator.Eq, id))
                   .ToArray();
            IPredicateGroup predicate = Predicates.Group(GroupOperator.Or, fieldPredicateList);
            return GetList(predicate);
        }

        public IEnumerable<TEntity> GetList(int skip, int take)
        {
            return GetList().Skip(skip).Take(take);
        }

        public int Create(TEntity entity)
        {
            using(IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                return db.Insert(entity);
            }
        }

        public void Delete(TEntity entity)
        {
            using(IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                db.Delete(entity);
            }
        }
        public void Delete(int entity_id)
        {
            using(IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                TEntity entity = Get(entity_id);
                Delete(entity);
            }
        }

        public void Delete(IPredicate predicate)
        {
            using(IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                db.Delete(predicate);
            }
        }

        public void Update(TEntity entity)
        {
            using(IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                db.Update(entity);
            }
        }

        protected int Count(IPredicate predicate = null)
        {
            using(IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                return db.Count<TEntity>(predicate);
            }
        }
        public int Count()
        {
            return Count(null);
        }

    }
}
