using PubLibIS.DAL.Interfaces;
using PubLibIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PubLibIS.DAL.Repositories.EntityFramework
{
    public class PeriodicalEditionRepository: IPeriodicalEditionRepository
    {
        private LibraryEntityFrameworkContext context;

        public PeriodicalEditionRepository(LibraryEntityFrameworkContext context)
        {
            this.context = context;
        }

        public int Create(PeriodicalEdition periodicalEdition)
        {
            periodicalEdition.Periodical = context.Periodicals.Find(periodicalEdition.Periodical.Id);
            context.PeriodicalEditions.Add(periodicalEdition);
            context.SaveChanges();
            return periodicalEdition.Id;
        }

        public void Delete(int periodicalEditionId)
        {
            var periodicalEdition = Get(periodicalEditionId);
            context.PeriodicalEditions.Remove(periodicalEdition);
        }

        public PeriodicalEdition Get(int periodicalEditionId)
        {
            return context.PeriodicalEditions.Find(periodicalEditionId);
        }

        public IEnumerable<PeriodicalEdition> GetList()
        {
            return context.PeriodicalEditions.AsEnumerable();
        }

        public IEnumerable<PeriodicalEdition> GetList(int skip, int take)
        {
            return context.PeriodicalEditions.Skip(skip).Take(take).AsEnumerable();
        }
    

        public void Update(PeriodicalEdition periodicalEdition)
        {
            var current = Get(periodicalEdition.Id);
            current.Periodical = context.Periodicals.Find(periodicalEdition.Periodical.Id);
            context.Entry(current).CurrentValues.SetValues(periodicalEdition);
        }

        public IEnumerable<PeriodicalEdition> GetPeriodicalEditionByPeriodicalId(int id)
        {
            return context.PeriodicalEditions.Where(pe=>pe.Periodical.Id == id).ToList();
        }
    }
}
