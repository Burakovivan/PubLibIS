using PubLibIS.DAL.Interfaces;
using PubLibIS.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PubLibIS.DAL.Repositories.EntityFramework
{
    public class BrochureRepository : IBrochureRepository
    {
        private LibraryEntityFrameworkContext context;

        public BrochureRepository(LibraryEntityFrameworkContext context)
        {
            this.context = context;
        }

        public int Count()
        {
            return context.Brochures.Count();
        }

        public int Create(Brochure brochure)
        {
            brochure.PublishingHouse = context.PublishingHouses.Find(brochure.PublishingHouse.Id);
            context.Brochures.Add(brochure);

            context.SaveChanges();
            return brochure.Id;
        }

        public void Delete(int brochureId)
        {
            var brochure = Get(brochureId);
            context.Brochures.Remove(brochure);
        }

        public Brochure Get(int brochureId)
        {
            return context.Brochures.Find(brochureId);
        }

        public IEnumerable<Brochure> GetList()
        {
            return context.Brochures.ToList();
        }

        public IEnumerable<Brochure> GetList(int skip, int take)
        {
            return context.Brochures.Skip(skip).Take(take).ToList();
        }

        public IEnumerable<Brochure> GetList(IEnumerable<int> idList)
        {
            return context.Brochures.Where(brochure => idList.Contains(brochure.Id)).ToList();
        }

        public void Update(Brochure brochure)
        {
            var current = Get(brochure.Id);
            current.PublishingHouse = context.PublishingHouses.Find(brochure.PublishingHouse?.Id ?? brochure.PublishingHouse_Id);
            context.Entry(current).CurrentValues.SetValues(brochure);
        }

    }
}
