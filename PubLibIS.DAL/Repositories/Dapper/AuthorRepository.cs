using PubLibIS.DAL.Interfaces;
using PubLibIS.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using DapperExtensions;

namespace PubLibIS.DAL.Repositories.Dapper
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(DapperConnectionFactory dapperConnectionFactory)
        : base(dapperConnectionFactory) { }
    }
}
