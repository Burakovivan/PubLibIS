using PubLibIS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubLibIS.DAL.Interfaces
{
    public interface ILazyLoad<TEntity> where TEntity : BaseEntity
    {
        void LoadNavigationProperties(TEntity entity, IDbConnection connection);
        void LoadNavigationProperties(IEnumerable<TEntity> entityList, IDbConnection connection);
    }
}
