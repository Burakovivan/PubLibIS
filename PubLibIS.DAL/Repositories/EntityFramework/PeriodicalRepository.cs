using PubLibIS.DAL.Interfaces;
using PubLibIS.DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace PubLibIS.DAL.Repositories.EntityFramework
{
    public class PeriodicalRepository: IPeriodicalRepository
    {
        private LibraryEntityFrameworkContext context;

        public PeriodicalRepository(LibraryEntityFrameworkContext context)
        {
            this.context = context;
        }

        public int Create(Periodical periodical)
        {
            context.Periodicals.Add(periodical);
            context.SaveChanges();
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
            return context.Periodicals.ToList();
        }

        public IEnumerable<Periodical> Read(int skip, int take)
        {
            return context.Periodicals.Skip(skip).Take(take).AsEnumerable();
        }

        public void Update(Periodical periodical)
        {
            var current = Read(periodical.Id);
            current.PublishingHouse = context.PublishingHouses.Find(periodical.PublishingHouse.Id);
            context.Entry(current).CurrentValues.SetValues(periodical);
        }
    }
}
