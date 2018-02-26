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

        public void Create(PeriodicalEdition periodicalEdition)
        {
            context.PeriodicalEditions.Add(periodicalEdition);
            context.SaveChanges();
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

        public void Update(PeriodicalEdition periodicalEdition)
        {
            context.Entry(periodicalEdition).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }
    }
}
