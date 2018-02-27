using AutoMapper;
using PubLibIS_DAL.Model;
using ViewModels;

namespace PubLibIS_BLL
{
    class MapperConfiguration
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Author, AuthorViewModel>();
                cfg.CreateMap<AuthorViewModel, Author>();
                cfg.CreateMap<PublishingHouseViewModel, PublishingHouse>();
                cfg.CreateMap<PublishingHouse, PublishingHouseViewModel>();
            });
        }
    }
}
