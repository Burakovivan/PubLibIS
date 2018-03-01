using AutoMapper;
using PubLibIS_DAL.Model;
using System.Collections.Generic;
using ViewModels;

namespace PubLibIS_BLL.Mappers
{
    public static class BrochureMapper
    {
        public static BrochureViewModel MapOneUp(Brochure brochure)
        {
            return Mapper.Map<Brochure, BrochureViewModel>(brochure);
        }

        public static IEnumerable<BrochureViewModel> MapManyUp(IEnumerable<Brochure> brochures)
        {
            return Mapper.Map<IEnumerable<Brochure>, IEnumerable<BrochureViewModel>>(brochures);
        }

        public static Brochure MapOneDown(BrochureViewModel brochure)
        {
            return Mapper.Map<BrochureViewModel, Brochure>(brochure);
        }

        public static IEnumerable<Brochure> MapManyUp(IEnumerable<BrochureViewModel> brochures)
        {
            return Mapper.Map<IEnumerable<BrochureViewModel>, IEnumerable<Brochure>>(brochures);
        }

    }
}
