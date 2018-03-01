using PubLibIS_DAL.IoC;
using System.Collections.Generic;
using ViewModels;

namespace PubLibIS_BLL.Services
{
    public class PeriodicalService
    {
        private LibraryRepository repos;

        public PeriodicalService()
        {
            repos = LibraryRepository.GetInstance();
        }

        public IEnumerable<PeriodicalViewModel> GetAll()
        {
            var periodicals = repos.PeriodicalRepository.Read();
            return Mappers.PeriodicalMapper.MapManyUp(periodicals);
        }

        public PeriodicalViewModel Get(int id)
        {
            var periodical = repos.PeriodicalRepository.Read(id);
            return Mappers.PeriodicalMapper.MapOneUp(periodical);
        }

        public void Delete(int id)
        {
            repos.PeriodicalRepository.Delete(id);
        }

        public void Update(PeriodicalViewModel periodical)
        {
            var mappedPeriodical = Mappers.PeriodicalMapper.MapOneDown(periodical);
            repos.PeriodicalRepository.Update(mappedPeriodical);
        }

        public int Create(PeriodicalViewModel periodical)
        {
            var mappedPeriodical = Mappers.PeriodicalMapper.MapOneDown(periodical);
            return repos.PeriodicalRepository.Create(mappedPeriodical);
        }

    }
}
