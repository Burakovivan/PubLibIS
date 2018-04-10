using AutoMapper;
using PubLibIS.Domain.Entities;
using PubLibIS.ViewModels;

namespace PubLibIS.BLL.MappingProfiles
{
    public class AuthorMappingProfile : Profile
    {
        public AuthorMappingProfile()
        {
            CreateMap<Author, AuthorViewModel>();
            CreateMap<AuthorViewModel, Author>();
        }
    }
}
