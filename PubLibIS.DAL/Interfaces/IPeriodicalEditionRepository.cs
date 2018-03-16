using PubLibIS.DAL.Models;
using System;
using System.Collections.Generic;

namespace PubLibIS.DAL.Interfaces
{
    public interface IPeriodicalEditionRepository
    {
        int Create(PeriodicalEdition periodicalEdition);
        PeriodicalEdition Get(int periodicalEditionId);
        IEnumerable<PeriodicalEdition> Get();
        IEnumerable<PeriodicalEdition> Get(int skip, int take);
        void Update(PeriodicalEdition periodicalEdition);
        void Delete(int periodicalEditionId);
        IEnumerable<PeriodicalEdition> GetPeriodicalEditionByPeriodicalId(int id);

    }
}
