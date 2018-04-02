using PubLibIS.DAL.UnitsOfWork;
using System.Collections.Generic;
using PubLibIS.ViewModels;
using PubLibIS.BLL.Interfaces;
using PubLibIS.DAL.Interfaces;
using AutoMapper;
using PubLibIS.DAL.Models;
using System.Linq;
using Newtonsoft.Json;
using PubLibIS.BLL.JsonModels;

namespace PubLibIS.BLL.Services
{
    public class BrochureService : IJsonProcessor
    {
        private IUnitOfWork db;
        private IMapper mapper;

        public BrochureService(IUnitOfWork uow, IMapper mapper)
        {
            db = uow;
            this.mapper = mapper;
        }

        public IEnumerable<BrochureViewModel> GetBrochureViewModelList()
        {
            IEnumerable<Brochure> brochures = db.Brochures.GetList();
            return mapper.Map<IEnumerable<Brochure>, IEnumerable<BrochureViewModel>>(brochures);
        }

        public BrochureViewModel GetBrochureViewModel(int id)
        {
            Brochure brochure = db.Brochures.Get(id);
            return mapper.Map<Brochure, BrochureViewModel>(brochure);
        }


        public void DeleteBrochure(int id)
        {
            db.Brochures.Delete(id);
            db.Save();
        }

        public void UpdateBrochure(BrochureViewModel brochure)
        {
            Brochure mappedBrochure = mapper.Map<BrochureViewModel, Brochure>(brochure);
            db.Brochures.Update(mappedBrochure);
            db.Save();
        }

        public int CreateBrochure(BrochureViewModel brochure)
        {
            Brochure mappedBrochure = mapper.Map<BrochureViewModel, Brochure>(brochure);
            var newId = db.Brochures.Create(mappedBrochure);
            db.Save();
            return newId;
        }

        public string GetJson(IEnumerable<int> idList)
        {
            var BrochureList = db.Brochures.GetList(idList).ToList();
            var result = JsonConvert.SerializeObject(new BrochureJsonAggregator { Brochures = BrochureList }, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, NullValueHandling = NullValueHandling.Ignore });
            return result;
        }

        public void SetJson(string json)
        {
            BrochureJsonAggregator deserRes = JsonConvert.DeserializeObject<BrochureJsonAggregator>(json);

            if(deserRes == null)
            {

                return;
            }

                foreach (Brochure brochure in deserRes.Brochures)
                {
                   var newId =  db.PublishingHouses.Create(brochure.PublishingHouse);
                    brochure.PublishingHouse_Id = newId;
                    db.Brochures.Create(brochure);
                }
                db.Save();
        }

        public BrochureCatalogViewModel GetBrochureCatalogViewModel(int skip, int take)
        {
            IEnumerable<Brochure> brochureList = db.Brochures.GetList().OrderBy(b => b.Id).Skip(skip).Take(take);

            var result = new BrochureCatalogViewModel
            {
                Brochures = mapper.Map<IEnumerable<Brochure>, IEnumerable<BrochureViewModel>>(brochureList),
                Skip = skip,
                IsSeeMore = brochureList.Count() < db.Brochures.Count(),
                HasNextPage = db.Brochures.Count() > skip + take
            };
            return result;
        }
    }
}
