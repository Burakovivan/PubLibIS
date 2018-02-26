using PubLibIS_DAL.Interfaces;
using PubLibIS_DAL.Model;
using System.Collections.Generic;
using System.Linq;

namespace PubLibIS_DAL.IoC.MSSQL
{
    public class PeriodicalRepository: IPeriodicalRepository
    {
        private LibraryContext context;

        public PeriodicalRepository(LibraryContext context)
        {
            this.context = context;
        }

        public void Create(Periodical periodical)
        {
            context.Periodicals.Add(periodical);
            context.SaveChanges();
        }

        public void Delete(int periodicalId)
        {
            var periodical = Read(periodicalId);
            context.Periodicals.Remove(periodical);
            context.SaveChanges();
        }

        public Periodical Read(int periodicalId)
        {
            return context.Periodicals.Find(periodicalId);
        }

        public IEnumerable<Periodical> Read()
        {
            return context.Periodicals.AsEnumerable();
        }

        public void Update(Periodical periodical)
        {
            context.Entry(periodical).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }
    }
}
