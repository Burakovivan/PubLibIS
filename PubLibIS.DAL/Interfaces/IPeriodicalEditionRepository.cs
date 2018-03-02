using PubLibIS.DAL.Model;
using System;
using System.Collections.Generic;

namespace PubLibIS.DAL.Interfaces
{
    public interface IPeriodicalEditionRepository
    {
        int Create(PeriodicalEdition periodicalEdition);
        PeriodicalEdition Read(int periodicalEditionId);
        IEnumerable<PeriodicalEdition> Read();
        IEnumerable<PeriodicalEdition> Read(int skip, int take);
        IEnumerable<PeriodicalEdition> ReadByPeriodicalId(int periodicalId);
        void Update(PeriodicalEdition periodicalEdition);
        void Delete(int periodicalEditionId);

        IEnumerable<PeriodicalEdition> Where(Func<PeriodicalEdition, bool> predicate);
    }
}
