using PubLibIS.DAL.UoW;
using System.Collections.Generic;
using PubLibIS.ViewModels;
using PubLibIS.BLL.Interfaces;
using System.Linq;
using PubLibIS.DAL.Interfaces;

namespace PubLibIS.BLL.Services
{
    public class PeriodicalService : IPeriodicalService
    {
        private IUnitOfWork db;

        public PeriodicalService(IUnitOfWork uow)
        {
            db = uow;
        }

        public IEnumerable<PeriodicalViewModel> GetPeriodicalViewModelList()
        {
            var periodicals = db.Periodicals.Read();
            return Mappers.PeriodicalMapper.MapManyUp(periodicals);
        }

        public PeriodicalViewModel GetPeriodicalViewModel(int id)
        {
            var periodical = db.Periodicals.Read(id);
            return Mappers.PeriodicalMapper.MapOneUp(periodical);
        }

        public void DeletePeriodical(int id)
        {
            db.Periodicals.Delete(id);
        }

        public void UpdatePeriodical(PeriodicalViewModel periodical)
        {
            var mappedPeriodical = Mappers.PeriodicalMapper.MapOneDown(periodical);
            db.Periodicals.Update(mappedPeriodical);
        }

        public int CreatePeriodical(PeriodicalViewModel periodical)
        {
            var mappedPeriodical = Mappers.PeriodicalMapper.MapOneDown(periodical);
            return db.Periodicals.Create(mappedPeriodical);
        }
        public int GetNextNumberByPeriodicalId(int periodicalId)
        {
            var editions = db.PeriodicalEditions.ReadByPeriodicalId(periodicalId);
            return editions.Select(x => x.ReleaseNumber).Max() + 1;
        }


        public IEnumerable<PublishingHouseViewModelSlim> GetPublishingHouseViewModelSlimList()
        {
            return db.PublishingHouses.Select(ph => new PublishingHouseViewModelSlim { Id = ph.Id, Description = $"{ph.Name} ({ph.Country}, {ph.City})" });
        }

        public IEnumerable<PeriodicalEditionViewModel> GetPeriodicalEditionViewModelByPeriodicalId(int periodicalId)
        {
            var periodical = db.PeriodicalEditions.Where(pe => pe.Periodical.Id == periodicalId);
            return Mappers.PeriodicalEditionMapper.MapManyUp(periodical);
        }

        public void UpdatePeriodicalEdition(PeriodicalEditionViewModel periodical)
        {
            var mappedPeriodical = Mappers.PeriodicalEditionMapper.MapOneDown(periodical);
            db.PeriodicalEditions.Update(mappedPeriodical);
        }

        public void CreatePeriodicalEdition(PeriodicalEditionViewModel edition)
        {
            var mappedEdition = Mappers.PeriodicalEditionMapper.MapOneDown(edition);
            db.PeriodicalEditions.Create(mappedEdition);
        }

        public void DeletePeriodicalEdition(int id)
        {
            db.PeriodicalEditions.Delete(id);
        }
    }
}
