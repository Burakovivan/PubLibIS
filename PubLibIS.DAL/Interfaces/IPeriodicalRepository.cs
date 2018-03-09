using PubLibIS.DAL.Models;
using System.Collections.Generic;

namespace PubLibIS.DAL.Interfaces
{
    public interface IPeriodicalRepository
    {
        int Create(Periodical periodical);
        Periodical Get(int periodicalId);
        IEnumerable<Periodical> Get();
        IEnumerable<Periodical> Get(int skip, int take);
        void Update(Periodical periodical);
        void Delete(int periodicalId);
    }
}
