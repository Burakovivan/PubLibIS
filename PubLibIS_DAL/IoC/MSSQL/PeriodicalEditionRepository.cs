using PubLibIS_DAL.Interfaces;
using PubLibIS_DAL.Model;
using System.Collections.Generic;
using System.Linq;

namespace PubLibIS_DAL.IoC.MSSQL
{
    public class PeriodicalEditionRepository: IPeriodicalEditionRepository
    {
        private LibraryContext context;

        public PeriodicalEditionRepository(LibraryContext context)
        {
            this.context = context;
        }

        public int Create(PeriodicalEdition periodicalEdition)
        {
            context.PeriodicalEditions.Add(periodicalEdition);
            context.SaveChanges();
            return periodicalEdition.Id;
        }

        public void Delete(int periodicalEditionId)
        {
            var periodicalEdition = Read(periodicalEditionId);
            context.PeriodicalEditions.Remove(periodicalEdition);
            context.SaveChanges();
        }

        public PeriodicalEdition Read(int periodicalEditionId)
        {
            return context.PeriodicalEditions.Find(periodicalEditionId);
        }

        public IEnumerable<PeriodicalEdition> Read()
        {
            return context.PeriodicalEditions.AsEnumerable();
        }

        public IEnumerable<PeriodicalEdition> Read(int skip, int take)
        {
            return context.PeriodicalEditions.Skip(skip).Take(take).AsEnumerable();
        }

        public void Update(PeriodicalEdition periodicalEdition)
        {
            var current = Read(periodicalEdition.Id);
            context.Entry(current).CurrentValues.SetValues(periodicalEdition);
            context.SaveChanges();
        }
    }
}
