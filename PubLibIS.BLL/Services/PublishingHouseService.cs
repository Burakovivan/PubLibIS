using PubLibIS.DAL.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PubLibIS.ViewModels;
using PubLibIS.BLL.Interfaces;
using PubLibIS.DAL.Interfaces;

namespace PubLibIS.BLL.Services
{
    public class PublishingHouseService : IPublishingHouseService
    {
        private IUnitOfWork repos;

        public PublishingHouseService(IUnitOfWork uow)
        {
            repos = uow;
        }

        public IEnumerable<PublishingHouseViewModel> GetPublishingHouseViewModelList()
        {
            var phs = repos.PublishingHouses.Read();
            return Mappers.PublishingHouseMapper.MapManyUp(phs);
        }

        public PublishingHouseViewModel GetPublishingHouseViewModel(int id)
        {
            var ph = repos.PublishingHouses.Read(id);
            return Mappers.PublishingHouseMapper.MapOneUp(ph);
        }

        public void PublishingHouse(int id)
        {
            repos.PublishingHouses.Delete(id);
        }

        public void UpdatePublishingHouse(PublishingHouseViewModel ph)
        {
            var mappedph = Mappers.PublishingHouseMapper.MapOneDown(ph);
            repos.PublishingHouses.Update(mappedph);
        }

        public int CreatePublishinHouse(PublishingHouseViewModel ph)
        {
            var mappedph = Mappers.PublishingHouseMapper.MapOneDown(ph);
            return repos.PublishingHouses.Create(mappedph);
        }
    }
}
