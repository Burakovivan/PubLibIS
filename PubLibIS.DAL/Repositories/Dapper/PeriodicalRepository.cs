using PubLibIS.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using PubLibIS.Domain.Entities;
using PubLibIS.DAL.ResponseModels;

namespace PubLibIS.DAL.Repositories.Dapper
{
    public class PeriodicalRepository : Repository<Periodical>, IPeriodicalRepository
    {

        public PeriodicalRepository(DapperConnectionFactory dapperConnectionFactory)
        : base(dapperConnectionFactory) { }

        public Periodical GetPeriodical(int periodicalId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Periodical> GetPeriodicalList()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Periodical> GetPeriodicalList(IEnumerable<int> idList)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Periodical> GetPeriodicalList(int skip, int take)
        {
            throw new System.NotImplementedException();
        }

        public GetPeriodicalResponseModel GetPeriodicalResponseModel(int periodicalId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<GetPeriodicalResponseModel> GetPeriodicalResponseModelList()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<GetPeriodicalResponseModel> GetPeriodicalResponseModelList(IEnumerable<int> idList)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<GetPeriodicalResponseModel> GetPeriodicalResponseModelList(int skip, int take)
        {
            IEnumerable<Periodical> periodicalList = GetList(skip, take);
            var periodicalEditionsRepo = new PeriodicalEditionRepository(dapperConnectionFactory);
            var publishingHouseRepo = new PublishingHouseRepository(dapperConnectionFactory);
            IEnumerable<GetPeriodicalResponseModel> response = periodicalList.Select(periodical =>
            {
                return new GetPeriodicalResponseModel
                {
                    Id = periodical.Id,
                    Foundation = periodical.Foundation,
                    IsPublished = periodical.IsPublished,
                    ISSN = periodical.ISSN,
                    Name = periodical.Name,
                    PublishingHouse_Id = periodical.PublishingHouse_Id,
                    PeriodicalEditions = periodicalEditionsRepo.GetPeriodicalEditionByPeriodicalId(periodical.Id),
                    PublishingHouse = periodical.PublishingHouse_Id.HasValue ? publishingHouseRepo.Get(periodical.PublishingHouse_Id.Value) : null
                };
            });

            return response;
        }
    }
}
