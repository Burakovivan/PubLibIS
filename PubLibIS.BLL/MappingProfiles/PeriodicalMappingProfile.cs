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
                    pvm => pvm.PeriodicalTypeSelectList,
                    opt => opt.Ignore())
                .ForMember(
                    pvm => pvm.Type,
                    opt => opt.MapFrom(pt => pt.Type))
                .ForMember(
                    pvm => pvm.PeriodicalEditions,
                    opt => opt.MapFrom(p => p.PeriodicalEditions))
                .ForMember(
                    pvm => pvm.Foundation,
                    opt => opt.MapFrom(p => p.Foundation))
                .ForMember(
                    pvm => pvm.Id,
                    opt => opt.MapFrom(p => p.Id))
                .ForMember(
                    pvm => pvm.IsPublished,
                    opt => opt.MapFrom(p => p.IsPublished))
                .ForMember(
                    pvm => pvm.ISSN,
                    opt => opt.MapFrom(p => p.ISSN))
                .ForMember(
                    pvm => pvm.Name,
                    opt => opt.MapFrom(p => p.Name))
                .ForMember(
                    pvm => pvm.PublishingHouse,
                    opt => opt.MapFrom(p => p.PublishingHouse))
                    .ForMember(
                    pvm => pvm.PublishingHouse_Id,
                    opt => opt.MapFrom(p => p.PublishingHouse_Id))
                    ;

            CreateMap<PeriodicalType, PeriodicalTypeViewModel>()
                .ConvertUsing(
                     (pt) => { return new PeriodicalTypeViewModel((int)pt, Enum.GetName(typeof(PeriodicalType), pt)); });

            CreateMap<PeriodicalTypeViewModel, PeriodicalType>()
               .ConvertUsing(
                    (ptvm) => { return (PeriodicalType)ptvm.Id; });
            //.ForMember(
            //    ptvm => ptvm.Id,
            //    opt => opt.MapFrom(pt => (int)pt))
            //.ForMember(
            //    ptvm => ptvm.Name,
            //    opt => opt.MapFrom(pt => Enum.GetName(typeof(PeriodicalType), pt)));


            CreateMap<PeriodicalViewModel, Periodical>()
                .ForMember(
                    pvm => pvm.PublishingHouse_Id,
                    opt => opt.MapFrom(p => p.PublishingHouse_Id))
                   ;

            CreateMap<PeriodicalEdition, PeriodicalEditionViewModel>()
                  .ForMember(
                    pevw => pevw.Periodical_Id,
                    opt => opt.MapFrom(pe => pe.Periodical_Id)
                    )
                  ;

            CreateMap<PeriodicalEditionViewModel, PeriodicalEdition>()
                .ForMember(
                    pe => pe.Periodical,
                    opt => opt.MapFrom(pevm => new Periodical { Id = pevm.Periodical_Id })
                    )
                     .ForMember(
                    pe => pe.Periodical_Id,
                    opt => opt.MapFrom(pevm => pevm.Periodical_Id)
                    )
                  ;
        }
    }
}
