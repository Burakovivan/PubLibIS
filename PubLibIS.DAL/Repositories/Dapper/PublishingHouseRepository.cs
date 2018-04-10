using PubLibIS.DAL.Interfaces;
using PubLibIS.Domain.Entities;

namespace PubLibIS.DAL.Repositories.Dapper
{
    public class PublishingHouseRepository : Repository<PublishingHouse>, IPublishingHouseRepository
    {
        public PublishingHouseRepository(DapperConnectionFactory dapperConnectionFactory)
        : base(dapperConnectionFactory) { }

    }
}
