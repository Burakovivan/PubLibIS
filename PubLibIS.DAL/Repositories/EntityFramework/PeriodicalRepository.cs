using PubLibIS.DAL.Interfaces;
using PubLibIS.DAL.ResponseModels;
using PubLibIS.Domain.Entities;
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
            var periodical = GetBook(periodicalId);
            context.Periodicals.Remove(periodical);
        }

        public Periodical GetBook(int periodicalId)
        {
            return context.Periodicals.Find(periodicalId);
        }

        public IEnumerable<Periodical> GetList()
        {
            return context.Periodicals.ToList();
        }

        public IEnumerable<Periodical> GetBookList(int skip, int take)
        {
            return context.Periodicals.OrderBy(p=>p.Id).Skip(skip).Take(take).AsEnumerable();
        }

        public IEnumerable<Periodical> GetPublishingHouseList(IEnumerable<int> idList)
        {
            return context.Periodicals.Where(periodical => idList.Contains(periodical.Id)).ToList();
        }

        public void Update(Periodical periodical)
        {
            var current = GetBook(periodical.Id);
            current.PublishingHouse = context.PublishingHouses.Find(periodical.PublishingHouse.Id);
            context.Entry(current).CurrentValues.SetValues(periodical);
        }

        public Periodical GetPeriodical(int periodicalId)
        {
            throw new System.NotImplementedException();
        }

        public GetPeriodicalResponseModel GetPeriodicalResponseModel(int periodicalId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Periodical> GetPeriodicalList()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Periodical> GetPeriodicalList(IEnumerable<int> idList)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Periodical> GetPeriodicalList(int skip, int take)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<GetPeriodicalResponseModel> GetPeriodicalResponseModelList()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<GetPeriodicalResponseModel> GetPeriodicalResponseModelList(IEnumerable<int> idList)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<GetPeriodicalResponseModel> GetPeriodicalResponseModelList(int skip, int take)
        {
            throw new System.NotImplementedException();
        }
    }
}
