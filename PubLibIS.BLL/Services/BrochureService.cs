using PubLibIS.DAL.UoW;
using System.Collections.Generic;
using PubLibIS.ViewModels;
using PubLibIS.BLL.Interfaces;
using PubLibIS.DAL.Interfaces;

namespace PubLibIS.BLL.Services
{
    public class BrochureService : IBrochureService
    {
        private IUnitOfWork db;

        public BrochureService(IUnitOfWork uow)
        {
            db = uow;
        }

        public IEnumerable<BrochureViewModel> GetGetBrochureViewModelList()
        {
            var brochures = db.Brochures.Read();
            return Mappers.BrochureMapper.MapManyUp(brochures);
        }

        public BrochureViewModel GetBrochureViewModel(int id)
        {
            var brochure = db.Brochures.Read(id);
            return Mappers.BrochureMapper.MapOneUp(brochure);
        }


        public void DeleteBrochure(int id)
        {
            db.Brochures.Delete(id);
        }

        public void UpdateBrochure(BrochureViewModel brochure)
        {
            var mappedBrochure = Mappers.BrochureMapper.MapOneDown(brochure);
            db.Brochures.Update(mappedBrochure);
        }

        public int CreateBrochure(BrochureViewModel brochure)
        {
            var mappedBrochure = Mappers.BrochureMapper.MapOneDown(brochure);
            return db.Brochures.Create(mappedBrochure);
        }

    }
}
