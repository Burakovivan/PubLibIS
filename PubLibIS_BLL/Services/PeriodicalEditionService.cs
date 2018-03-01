using PubLibIS_DAL.IoC;
using System.Collections.Generic;
using ViewModels;

namespace PubLibIS_BLL.Services
{
    public class PeriodicalEditionService
    {
        private LibraryRepository repos;

        public PeriodicalEditionService()
        {
            repos = LibraryRepository.GetInstance();
        }

        public IEnumerable<PeriodicalEditionViewModel> GetAll()
        {
            var periodicalEditions = repos.PeriodicalEditionRepository.Read();
            return Mappers.PeriodicalEditionMapper.MapManyUp(periodicalEditions);
        }

        public PeriodicalEditionViewModel Get(int id)
        {
            var periodical = repos.PeriodicalEditionRepository.Read(id);
            return Mappers.PeriodicalEditionMapper.MapOneUp(periodical);
        }

        public void Delete(int id)
        {
            repos.PeriodicalEditionRepository.Delete(id);
        }

        public void Update(PeriodicalEditionViewModel periodicalEdition)
        {
            var mappedPeriodical = Mappers.PeriodicalEditionMapper.MapOneDown(periodicalEdition);
            repos.PeriodicalEditionRepository.Update(mappedPeriodical);
        }

        public int Create(PeriodicalEditionViewModel periodicalEdition)
        {
            var mappedPeriodical = Mappers.PeriodicalEditionMapper.MapOneDown(periodicalEdition);
            return repos.PeriodicalEditionRepository.Create(mappedPeriodical);
        }

    }
}
