using PubLibIS.DAL.Models;
using System.Collections.Generic;

namespace PubLibIS.DAL.Interfaces
{
    public interface IPeriodicalRepository
    {
        int Create(Periodical periodical);
        int Count();
        Periodical Get(int periodicalId);
        IEnumerable<Periodical> GetList();
        IEnumerable<Periodical> GetList(IEnumerable<int> idList);
        IEnumerable<Periodical> GetList(int skip, int take);
        void Update(Periodical periodical);
        void Delete(int periodicalId);
    }
}
