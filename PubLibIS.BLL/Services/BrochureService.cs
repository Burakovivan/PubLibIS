using PubLibIS.DAL.UnitsOfWork;
using System.Collections.Generic;
using PubLibIS.ViewModels;
using PubLibIS.BLL.Interfaces;
using PubLibIS.DAL.Interfaces;
using AutoMapper;
using PubLibIS.DAL.Models;

namespace PubLibIS.BLL.Services
{
    public class BrochureService : IBrochureService
    {
        private IUnitOfWork db;
        private IMapper mapper;

        public BrochureService(IUnitOfWork uow, IMapper mapper)
        {
            db = uow;
            this.mapper = mapper;
        }

        public IEnumerable<BrochureViewModel> GetGetBrochureViewModelList()
        {
            var brochures = db.Brochures.Get();
            return mapper.Map<IEnumerable<Brochure>, IEnumerable<BrochureViewModel>>(brochures);
        }

        public BrochureViewModel GetBrochureViewModel(int id)
        {
            var brochure = db.Brochures.Get(id);
            return mapper.Map<Brochure, BrochureViewModel>(brochure);
        }


        public void DeleteBrochure(int id)
        {
            db.Brochures.Delete(id);
            db.Save();
        }

        public void UpdateBrochure(BrochureViewModel brochure)
        {
            var mappedBrochure = mapper.Map<BrochureViewModel, Brochure>(brochure);
            db.Brochures.Update(mappedBrochure);
            db.Save();
        }

        public int CreateBrochure(BrochureViewModel brochure)
        {
            var mappedBrochure = mapper.Map<BrochureViewModel, Brochure>(brochure);
            var newId = db.Brochures.Create(mappedBrochure);
            db.Save();
            return newId;
        }

        public string GetJson(IEnumerable<int> idList)
        {
            throw new System.NotImplementedException();
        }

        public void SetJson(string IdList)
        {
            throw new System.NotImplementedException();
        }
    }
}
