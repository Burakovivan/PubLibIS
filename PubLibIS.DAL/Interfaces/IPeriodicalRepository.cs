using PubLibIS.DAL.Models;
using System.Collections.Generic;

namespace PubLibIS.DAL.Interfaces
{
    public interface IPeriodicalRepository
    {
        int Create(Periodical periodical);
        int Count();
        Periodical Get(int periodicalId);
        IEnumerable<Periodical> Get();
        IEnumerable<Periodical> Get(IEnumerable<int> idList);
        IEnumerable<Periodical> Get(int skip, int take);
        void Update(Periodical periodical);
        void Delete(int periodicalId);
    }
}
