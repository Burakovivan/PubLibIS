using AutoMapper;
using PubLibIS.DAL.Models;
using PubLibIS.ViewModels;
using System;

namespace PubLibIS.BLL.MappingProfiles
{
    public class PeriodicalMappingProfile : Profile
    {
        public PeriodicalMappingProfile()
        {
            CreateMap<Periodical, PeriodicalViewModel>()

                .ForMember(
                    pvm => pvm.PublishingHouseSelectList,
                    opt => opt.Ignore())
                .ForMember(
                    pvm => pvm.Type,
                    opt => opt.MapFrom(p => p.Type))
                .ForMember(
                    pvm => pvm.PeriodicalEditions,
                    opt => opt.MapFrom(p => p.PeriodicalEditions))
                    ;

            //CreateMap<PeriodicalType, PeriodicalTypeViewModel>()
            //    .ForMember(
            //        ptvm => ptvm.Id,
            //        opt => opt.MapFrom(pt => (int)pt))
            //    .ForMember(
            //        ptvm => ptvm.Name,
            //        opt => opt.MapFrom(pt => Enum.GetName(typeof(PeriodicalType), pt)));


            CreateMap<PeriodicalViewModel, Periodical>()
                .ForMember(
                    p => p.Type,
                    opt => opt.MapFrom(pvm => pvm.Type.Id));

            CreateMap<PeriodicalEdition, PeriodicalEditionViewModel>();
            CreateMap<PeriodicalEditionViewModel, PeriodicalEdition>();
        }
    }
}
