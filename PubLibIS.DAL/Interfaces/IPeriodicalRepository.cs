using PubLibIS.DAL.ResponseModels;
using PubLibIS.Domain.Entities;
using System.Collections.Generic;

namespace PubLibIS.DAL.Interfaces
{
    public interface IPeriodicalRepository
    {
        int Create(Periodical periodical);
        int Count();
        Periodical GetPeriodical(int periodicalId);
        GetPeriodicalResponseModel GetPeriodicalResponseModel(int periodicalId);
        IEnumerable<Periodical> GetPeriodicalList();
        IEnumerable<Periodical> GetPeriodicalList(IEnumerable<int> idList);
        IEnumerable<Periodical> GetPeriodicalList(int skip, int take);
        IEnumerable<GetPeriodicalResponseModel> GetPeriodicalResponseModelList();
        IEnumerable<GetPeriodicalResponseModel> GetPeriodicalResponseModelList(IEnumerable<int> idList);
        IEnumerable<GetPeriodicalResponseModel> GetPeriodicalResponseModelList(int skip, int take);
        void Update(Periodical periodical);
        void Delete(int periodicalId);
    }
}
