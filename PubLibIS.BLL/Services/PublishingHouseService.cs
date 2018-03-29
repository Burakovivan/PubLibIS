using PubLibIS.DAL.UnitsOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PubLibIS.ViewModels;
using PubLibIS.BLL.Interfaces;
using PubLibIS.DAL.Interfaces;
using AutoMapper;
using PubLibIS.DAL.Models;
using Newtonsoft.Json;
using PubLibIS.BLL.JsonModels;

namespace PubLibIS.BLL.Services
{
    public class PublishingHouseService : IPublishingHouseService
    {
        private IUnitOfWork db;
        private IMapper mapper;

        public PublishingHouseService(IUnitOfWork uow, IMapper mapper)
        {
            db = uow;
            this.mapper = mapper;
        }

        public IEnumerable<PublishingHouseViewModel> GetPublishingHouseViewModelList()
        {
            var phs = db.PublishingHouses.GetList();
            return mapper.Map<IEnumerable<PublishingHouse>, IEnumerable<PublishingHouseViewModel>>(phs);
        }

        public PublishingHouseViewModel GetPublishingHouseViewModel(int id)
        {
            var ph = db.PublishingHouses.Get(id);
            return mapper.Map<PublishingHouse, PublishingHouseViewModel>(ph);
        }

        public void DeletePublishingHouse(int id)
        {
            db.PublishingHouses.Delete(id);
            db.Save();
        }

        public void UpdatePublishingHouse(PublishingHouseViewModel ph)
        {
            var mappedph = mapper.Map<PublishingHouseViewModel, PublishingHouse>(ph);
            db.PublishingHouses.Update(mappedph);
            db.Save();
        }

        public int CreatePublishingHouse(PublishingHouseViewModel ph)
        {
            var mappedph = mapper.Map<PublishingHouseViewModel, PublishingHouse>(ph);
            var newId = db.PublishingHouses.Create(mappedph);
            db.Save();
            return newId;
        }

        public IEnumerable<PublishingHouseViewModelSlim> GetPublishingHouseViewModelSlimList()
        {
            return mapper.Map<IEnumerable<PublishingHouse>, IEnumerable<PublishingHouseViewModelSlim>>(db.PublishingHouses.GetList());
        }

        public string GetJson(IEnumerable<int> idList)
        {
            var PublishingHouseList = db.PublishingHouses.GetList(idList).ToList();
            var result = JsonConvert.SerializeObject(new PublishingHouseJsonAggregator { PublishingHouses = PublishingHouseList }, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, NullValueHandling = NullValueHandling.Ignore });
            return result;
        }

        public void SetJson(string json)
        {
            var deserRes = JsonConvert.DeserializeObject<PublishingHouseJsonAggregator>(json);

            if(deserRes == null)
            {
                return;
            }
            foreach(var зublishingHouse in deserRes.PublishingHouses)
            {
                db.PublishingHouses.Create(зublishingHouse);
            }
            db.Save();
        }
    }
}
