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

        public int Count()
        {
            return context.Periodicals.Count();
        }

        public int Create(Periodical periodical)
        {
            context.Periodicals.Add(periodical);
            context.SaveChanges();
            return periodical.Id;
        }

        public void Delete(int periodicalId)
        {
            var periodical = Get(periodicalId);
            context.Periodicals.Remove(periodical);
        }

        public Periodical Get(int periodicalId)
        {
            return context.Periodicals.Find(periodicalId);
        }

        public IEnumerable<Periodical> GetList()
        {
            return context.Periodicals.ToList();
        }

        public IEnumerable<Periodical> GetList(int skip, int take)
        {
            return context.Periodicals.OrderBy(p=>p.Id).Skip(skip).Take(take).AsEnumerable();
        }

        public IEnumerable<Periodical> GetList(IEnumerable<int> idList)
        {
            return context.Periodicals.Where(periodical => idList.Contains(periodical.Id)).ToList();
        }

        public void Update(Periodical periodical)
        {
            var current = Get(periodical.Id);
            current.PublishingHouse = context.PublishingHouses.Find(periodical.PublishingHouse.Id);
            context.Entry(current).CurrentValues.SetValues(periodical);
        }
    }
}
