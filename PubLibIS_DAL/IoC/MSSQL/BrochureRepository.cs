using PubLibIS_DAL.Interfaces;
using PubLibIS_DAL.Model;
using System.Collections.Generic;
using System.Linq;

namespace PubLibIS_DAL.IoC.MSSQL
{
    public class BrochureRepository : IBrochureRepository
    {
        private LibraryContext context;

        public BrochureRepository(LibraryContext context)
        {
            this.context = context;
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
            var brochure = Read(brochureId);
            context.Brochures.Remove(brochure);
            context.SaveChanges();
        }

        public Brochure Read(int brochureId)
        {
            return context.Brochures.Find(brochureId);
        }

        public IEnumerable<Brochure> Read()
        {
            return context.Brochures.AsEnumerable();
        }

        public IEnumerable<Brochure> Read(int skip, int take)
        {
            return context.Brochures.Skip(skip).Take(take).AsEnumerable();
        }

        public void Update(Brochure brochure)
        {
            var current = Read(brochure.Id);
            current.PublishingHouse = context.PublishingHouses.Find(brochure.PublishingHouse.Id);
            context.Entry(current).CurrentValues.SetValues(brochure);
            context.SaveChanges();
        }
      
    }
}
