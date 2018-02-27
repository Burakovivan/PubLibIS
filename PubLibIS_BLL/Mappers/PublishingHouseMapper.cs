using AutoMapper;
using PubLibIS_DAL.Model;
using System.Collections.Generic;
using ViewModels;

namespace PubLibIS_BLL.Mappers
{
    public static class PublishingHouseMapper
    {
        public static PublishingHouseViewModel MapOneUp(PublishingHouse ph)
        {
            return Mapper.Map<PublishingHouse, PublishingHouseViewModel>(ph);
        }

        public static IEnumerable<PublishingHouseViewModel> MapManyUp(IEnumerable<PublishingHouse> phs)
        {
            return Mapper.Map<IEnumerable<PublishingHouse>, IEnumerable<PublishingHouseViewModel>>(phs);
        }

        public static PublishingHouse MapOneDown(PublishingHouseViewModel ph)
        {
            return Mapper.Map<PublishingHouseViewModel, PublishingHouse>(ph);
        }

        public static IEnumerable<PublishingHouse> MapManyUp(IEnumerable<PublishingHouseViewModel> phs)
        {
            return Mapper.Map<IEnumerable<PublishingHouseViewModel>, IEnumerable<PublishingHouse>>(phs);
        }

    }
}
