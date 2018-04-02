using PubLibIS.DAL.Interfaces;
using PubLibIS.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;

namespace PubLibIS.DAL.Repositories.Dapper
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(DapperConnectionFactory dapperConnectionFactory)
        : base(dapperConnectionFactory) { }

        public override void LoadNavigationProperties(Author entity, IDbConnection connection)
        {
            if(entity.Books == null && SuperReference!= typeof(AuthorInBook))
            {
                var repo = new AuthorInBookRepository(dapperConnectionFactory);
                repo.SuperReference = typeof(Author);
                entity.Books = repo.GetByAuthorId(entity.Id).ToList();
            }
        }


    }
}
