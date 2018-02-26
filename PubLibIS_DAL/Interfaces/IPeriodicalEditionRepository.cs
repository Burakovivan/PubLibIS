using PubLibIS_DAL.Model;
using System.Collections.Generic;

namespace PubLibIS_DAL.Interfaces
{
    public interface IPeriodicalEditionRepository
    {
        void Create(PeriodicalEdition periodicalEdition);
        PeriodicalEdition Read(int periodicalEditionId);
        IEnumerable<PeriodicalEdition> Read();
        void Update(PeriodicalEdition periodicalEdition);
        void Delete(int periodicalEditionId);
    }
}
