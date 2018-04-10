using PubLibIS.DAL.Interfaces;
using PubLibIS.Domain.Entities;

namespace PubLibIS.DAL.Repositories.Dapper
{
    public class BrochureRepository : Repository<Brochure>, IBrochureRepository
    {

        public BrochureRepository(DapperConnectionFactory dapperConnectionFactory)
        : base(dapperConnectionFactory) { }

    }
}
