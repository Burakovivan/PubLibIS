using AutoMapper;
using PubLibIS.Domain.Entities;
using PubLibIS.ViewModels;

namespace PubLibIS.BLL.MappingProfiles
{
    public class BrochureMappingProfile : Profile
    {
        public BrochureMappingProfile()
        {
            CreateMap<BrochureViewModel, Brochure>();
            CreateMap<Brochure, BrochureViewModel>();
        }
    }
}
