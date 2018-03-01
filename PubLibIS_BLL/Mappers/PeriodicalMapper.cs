using AutoMapper;
using PubLibIS_DAL.Model;
using System.Collections.Generic;
using ViewModels;

namespace PubLibIS_BLL.Mappers
{
    public static class PeriodicalMapper
    {
        public static PeriodicalViewModel MapOneUp(Periodical periodical)
        {
            return Mapper.Map<Periodical, PeriodicalViewModel>(periodical);
        }

        public static IEnumerable<PeriodicalViewModel> MapManyUp(IEnumerable<Periodical> periodicals)
        {
            return Mapper.Map<IEnumerable<Periodical>, IEnumerable<PeriodicalViewModel>>(periodicals);
        }

        public static Periodical MapOneDown(PeriodicalViewModel periodical)
        {
            return Mapper.Map<PeriodicalViewModel, Periodical>(periodical);
        }

        public static IEnumerable<Periodical> MapManyUp(IEnumerable<PeriodicalViewModel> periodicals)
        {
            return Mapper.Map<IEnumerable<PeriodicalViewModel>, IEnumerable<Periodical>>(periodicals);
        }

    }
}
