using PubLibIS.DAL.Model;
using System.Collections.Generic;

namespace PubLibIS.DAL.Interfaces
{
    public interface IPeriodicalRepository
    {
        int Create(Periodical periodical);
        Periodical Read(int periodicalId);
        IEnumerable<Periodical> Read();
        IEnumerable<Periodical> Read(int skip, int take);
        void Update(Periodical periodical);
        void Delete(int periodicalId);
    }
}
