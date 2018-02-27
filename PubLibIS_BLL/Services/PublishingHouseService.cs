using PubLibIS_DAL.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace PubLibIS_BLL.Services
{
    public class PublishingHouseService
    {
        private LibraryRepository repos;

        public PublishingHouseService()
        {
            repos = LibraryRepository.GetInstance();
        }

        public IEnumerable<PublishingHouseViewModel> GetAll()
        {
            var phs = repos.PublishingHouseRepository.Read();
            return Mappers.PublishingHouseMapper.MapManyUp(phs);
        }

        public PublishingHouseViewModel Get(int id)
        {
            var ph = repos.PublishingHouseRepository.Read(id);
            return Mappers.PublishingHouseMapper.MapOneUp(ph);
        }

        public void Delete(int id)
        {
            repos.PublishingHouseRepository.Delete(id);
        }

        public void Update(PublishingHouseViewModel ph)
        {
            var mappedph = Mappers.PublishingHouseMapper.MapOneDown(ph);
            repos.PublishingHouseRepository.Update(mappedph);
        }

        public int Create(PublishingHouseViewModel ph)
        {
            var mappedph = Mappers.PublishingHouseMapper.MapOneDown(ph);
            return repos.PublishingHouseRepository.Create(mappedph);
        }
    }
}
