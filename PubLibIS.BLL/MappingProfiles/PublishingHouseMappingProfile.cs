using AutoMapper;
using PubLibIS.Domain.Entities;
using PubLibIS.ViewModels;

namespace PubLibIS.BLL.MappingProfiles
{
    public class PublishingHouseMappingProfile : Profile
    {
        public PublishingHouseMappingProfile()
        {
            CreateMap<PublishingHouseViewModel, PublishingHouse>();
            CreateMap<PublishingHouse, PublishingHouseViewModel>();

            CreateMap<PublishingHouse, PublishingHouseViewModelSlim>()
                .ForMember(
                    phvms => phvms.Description,
                    opt => opt.MapFrom(ph => $"{ph.Name} ({ph.Country}, {ph.City})"));
        }
    }
}
