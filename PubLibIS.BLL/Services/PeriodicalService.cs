using PubLibIS.DAL.UnitsOfWork;
using System.Collections.Generic;
using PubLibIS.ViewModels;
using PubLibIS.BLL.Interfaces;
using System.Linq;
using PubLibIS.DAL.Interfaces;
using AutoMapper;
using PubLibIS.DAL.Models;
using System;

namespace PubLibIS.BLL.Services
{
    public class PeriodicalService : IPeriodicalService
    {
        private IUnitOfWork db;
        private IMapper mapper;

        public PeriodicalService(IUnitOfWork uow, IMapper mapper)
        {
            db = uow;
            this.mapper = mapper;
        }

        public IEnumerable<PeriodicalViewModel> GetPeriodicalViewModelList()
        {
            var periodicals = db.Periodicals.Get();
            var ptvm = mapper.Map<PeriodicalType, PeriodicalTypeViewModel>(PeriodicalType.magazine);
            return mapper.Map<IEnumerable<Periodical>, IEnumerable<PeriodicalViewModel>>(periodicals);
        }

        public PeriodicalViewModel GetPeriodicalViewModel(int id)
        {
            var periodical = db.Periodicals.Get(id);
            return mapper.Map<Periodical, PeriodicalViewModel>(periodical);
        }

        public void DeletePeriodical(int id)
        {
            db.Periodicals.Delete(id);
            db.Save();
        }

        public void UpdatePeriodical(PeriodicalViewModel periodical)
        {
            var mappedPeriodical = mapper.Map<PeriodicalViewModel, Periodical>(periodical);
            db.Periodicals.Update(mappedPeriodical);
            db.Save();
        }

        public int CreatePeriodical(PeriodicalViewModel periodical)
        {
            var mappedPeriodical = mapper.Map<PeriodicalViewModel, Periodical>(periodical);
            var newId = db.Periodicals.Create(mappedPeriodical);
            db.Save();
            return newId;
        }

        public int GetNextEditionNumberByPeriodicalId(int periodicalId)
        {
            var editions = db.PeriodicalEditions.GetByPeriodicalId(periodicalId);
            return editions.Any()? editions.Select(x => x.ReleaseNumber).Max() + 1: 1;
        }
        
             

        public IEnumerable<PeriodicalEditionViewModel> GetPeriodicalEditionViewModelListByPeriodicalId(int periodicalId)
        {
            var periodical = db.PeriodicalEditions.GetPeriodicalEditionByPeriodicalId(periodicalId);
            periodical = periodical.OrderBy(p => p.ReleaseNumber).ToList();
            return mapper.Map<IEnumerable<PeriodicalEdition>, IEnumerable<PeriodicalEditionViewModel>>(periodical);
        }

        public void UpdatePeriodicalEdition(PeriodicalEditionViewModel periodical)
        {
            var mappedPeriodical = mapper.Map<PeriodicalEditionViewModel, PeriodicalEdition>(periodical);
            db.PeriodicalEditions.Update(mappedPeriodical);
            db.Save();
        }

        public void CreatePeriodicalEdition(PeriodicalEditionViewModel edition)
        {
            var mappedEdition = mapper.Map<PeriodicalEditionViewModel, PeriodicalEdition>(edition);
            var newId = db.PeriodicalEditions.Create(mappedEdition);
            db.Save();
        }

        public void DeletePeriodicalEdition(int id)
        {
            db.PeriodicalEditions.Delete(id);
            db.Save();
        }

        public IEnumerable<PeriodicalTypeViewModel> GetPeriodicalTypeViewModelList()
        {
            var typesList = new List<PeriodicalTypeViewModel>();
            foreach(var name in Enum.GetNames(typeof(PeriodicalType)))
            {
                if (Enum.TryParse(name, true, out PeriodicalType pt))
                    typesList.Add(new PeriodicalTypeViewModel {Id = (int)pt, Name = name });
            }
            return typesList;

        }

        public PeriodicalEditionViewModel GetPeriodicalEditionViewModel(int id)
        {
            var pe = db.PeriodicalEditions.Get(id);
            return mapper.Map<PeriodicalEdition, PeriodicalEditionViewModel>(pe);
        }

        public string GetJson(IEnumerable<int> idList)
        {
            throw new NotImplementedException();
        }

        public void SetJson(string IdList)
        {
            throw new NotImplementedException();
        }
    }
}
