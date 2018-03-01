using PubLibIS_DAL.IoC;
using System.Collections.Generic;
using ViewModels;

namespace PubLibIS_BLL.Services
{
    public class BrochureService
    {
        private LibraryRepository repos;

        public BrochureService()
        {
            repos = LibraryRepository.GetInstance();
        }

        public IEnumerable<BrochureViewModel> GetAll()
        {
            var brochures = repos.BrochureRepository.Read();
            return Mappers.BrochureMapper.MapManyUp(brochures);
        }

        public BrochureViewModel Get(int id)
        {
            var brochure = repos.BrochureRepository.Read(id);
            return Mappers.BrochureMapper.MapOneUp(brochure);
        }


        public void Delete(int id)
        {
            repos.BrochureRepository.Delete(id);
        }

        public void Update(BrochureViewModel brochure)
        {
            var mappedBrochure = Mappers.BrochureMapper.MapOneDown(brochure);
            repos.BrochureRepository.Update(mappedBrochure);
        }

        public int Create(BrochureViewModel brochure)
        {
            var mappedBrochure = Mappers.BrochureMapper.MapOneDown(brochure);
            return repos.BrochureRepository.Create(mappedBrochure);
        }

    }
}
