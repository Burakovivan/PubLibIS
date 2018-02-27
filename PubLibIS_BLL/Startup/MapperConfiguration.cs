using AutoMapper;
using PubLibIS_DAL.Model;
using System.Linq;
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

                cfg.CreateMap<BookViewModel, Book>();
                cfg.CreateMap<Book, BookViewModel>()
                    .ForMember(
                        bookVM => bookVM.ReleaseDate,
                            opt => opt.MapFrom(book => book.PublishedBooks.Any() ? new System.DateTime?(book.PublishedBooks.Select(x => x.DateOfPublication).Min()) : null))
                    .ForMember(
                        bookVM => bookVM.Authors,
                        opt => opt.MapFrom(book => book.Authors.Select(x => x.Author)));

                cfg.CreateMap<PublishedBook, PublishedBookViewModel>()
                    .ForMember(
                        pBook => pBook.Book,
                        opt => opt.MapFrom(pBookVM => Mappers.BookMapper.MapOneUp(pBookVM.Book)));



                cfg.CreateMap<PublishedBookViewModel, PublishedBook>()
                    .ForMember(
                        pBookVM => pBookVM.Book,
                        opt => opt.MapFrom(pBook => Mappers.BookMapper.MapOneDown(pBook.Book)));
            });
        }
    }
}
