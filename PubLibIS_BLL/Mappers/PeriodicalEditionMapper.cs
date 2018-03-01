using AutoMapper;
using PubLibIS_DAL.Model;
using System.Collections.Generic;
using ViewModels;

namespace PubLibIS_BLL.Mappers
{
    public static class PeriodicalEditionMapper
    {
        public static PeriodicalEditionViewModel MapOneUp(PeriodicalEdition PE)
        {
            return Mapper.Map<PeriodicalEdition, PeriodicalEditionViewModel>(PE);
        }

        public static IEnumerable<PeriodicalEditionViewModel> MapManyUp(IEnumerable<PeriodicalEdition> PEs)
        {
            return Mapper.Map<IEnumerable<PeriodicalEdition>, IEnumerable<PeriodicalEditionViewModel>>(PEs);
        }

        public static PeriodicalEdition MapOneDown(PeriodicalEditionViewModel PE)
        {
            return Mapper.Map<PeriodicalEditionViewModel, PeriodicalEdition>(PE);
        }

        public static IEnumerable<PeriodicalEdition> MapManyUp(IEnumerable<PeriodicalEditionViewModel> PEs)
        {
            return Mapper.Map<IEnumerable<PeriodicalEditionViewModel>, IEnumerable<PeriodicalEdition>>(PEs);
        }

    }
}
