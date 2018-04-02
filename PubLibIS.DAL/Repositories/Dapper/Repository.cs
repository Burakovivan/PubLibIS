using PubLibIS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using PubLibIS.DAL.Interfaces;
using Dapper.Contrib.Extensions;

namespace PubLibIS.DAL.Repositories.Dapper
{
    public abstract class Repository<TEntity> : ILazyLoad<TEntity> where TEntity : BaseEntity
    {
        protected DapperConnectionFactory dapperConnectionFactory;
        public Type SuperReference;

        public Repository(DapperConnectionFactory dapperConnectionFactory)
        {
            this.dapperConnectionFactory = dapperConnectionFactory;
        }

        public TEntity Get(int id)
        {
            using(IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                TEntity result = db.Get<TEntity>(id);
                LoadNavigationProperties(result, db);
                return result;
            }
        }


        public IEnumerable<TEntity> GetList()
        {
            using(IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                List<TEntity> result = db.GetAll<TEntity>().ToList();
                LoadNavigationProperties(result, db);
                return result;
            }
        }

        public IEnumerable<TEntity> GetList(IEnumerable<int> idList)
        {
            return idList.Select(id => Get(id)).ToList();
        }

        public IEnumerable<TEntity> GetList(int skip, int take)
        {
            return GetList().Skip(skip).Take(take);
        }

        public int Create(TEntity entity)
        {
            using(IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                return (int)db.Insert(entity);
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



        public void Update(TEntity entity)
        {
            using(IDbConnection db = dapperConnectionFactory.GetConnectionInstance())
            {
                db.Update(entity);
            }
        }

        public int Count()
        {

            return GetList().Count();
        }
        public abstract void LoadNavigationProperties(TEntity entity, IDbConnection connection);

        public void LoadNavigationProperties(IEnumerable<TEntity> entityList, IDbConnection connection)
        {
            entityList.ToList().ForEach(entity => LoadNavigationProperties(entity, connection));
        }

    }
}
