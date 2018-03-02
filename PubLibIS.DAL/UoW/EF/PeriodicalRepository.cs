using PubLibIS.DAL.Interfaces;
using PubLibIS.DAL.Model;
using System.Collections.Generic;
using System.Linq;

namespace PubLibIS.DAL.UoW.EF
{
    public class PeriodicalRepository: IPeriodicalRepository
    {
        private LibraryContext context;

        public PeriodicalRepository(LibraryContext context)
        {
            this.context = context;
        }

        public int Create(Periodical periodical)
        {
            context.Periodicals.Add(periodical);
            return periodical.Id;
        }

        public void Delete(int periodicalId)
        {
            var periodical = Read(periodicalId);
            context.Periodicals.Remove(periodical);
        }

        public Periodical Read(int periodicalId)
        {
            return context.Periodicals.Find(periodicalId);
        }

        public IEnumerable<Periodical> Read()
        {
            return context.Periodicals.AsEnumerable();
        }

        public IEnumerable<Periodical> Read(int skip, int take)
        {
            return context.Periodicals.Skip(skip).Take(take).AsEnumerable();
        }

        public void Update(Periodical periodical)
        {
            var current = Read(periodical.Id);
            context.Entry(current).CurrentValues.SetValues(periodical);
        }
    }
}
