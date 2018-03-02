using AutoMapper;
using PubLibIS.DAL.Model;
using System.Linq;
using PubLibIS.ViewModels;

namespace PubLibIS.BLL
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

                cfg.CreateMap<BookViewModel, Book>()
                .ForMember(
                        book => book.Authors,
                        opt => opt.MapFrom(bookVM => bookVM.Authors));
                cfg.CreateMap<Book, BookViewModel>()
                    .ForMember(
                        bookVM => bookVM.ReleaseDate,
                            opt => opt.MapFrom(book => book.PublishedBooks.Any() ? book.PublishedBooks.Select(x => x.DateOfPublication).Min() : null))
                    .ForMember(
                        bookVM => bookVM.Authors,
                        opt => opt.MapFrom(book => book.Authors.Select(x => x.Author)));

                cfg.CreateMap<PublishedBook, PublishedBookViewModel>()
                    .ForMember(
                        pBook => pBook.Book,
                        opt => opt.MapFrom(pBookVM => Mappers.BookMapper.MapOneUp(pBookVM.Book)));

                cfg.CreateMap<AuthorViewModel, AuthorInBook>()
                .ForMember(
                    ainb => ainb.Author,
                    opt => opt.MapFrom(avm => avm));

                cfg.CreateMap<PublishedBookViewModel, PublishedBook>()
                    .ForMember(
                        pBookVM => pBookVM.Book,
                        opt => opt.MapFrom(pBook => Mappers.BookMapper.MapOneDown(pBook.Book)));


                cfg.CreateMap<Article, ArticleViewModel>();
                cfg.CreateMap<ArticleViewModel, Article>();

                cfg.CreateMap<Periodical, PeriodicalViewModel>();
                cfg.CreateMap<PeriodicalViewModel, Periodical>();

                cfg.CreateMap<PeriodicalEdition, PeriodicalEditionViewModel>();
                cfg.CreateMap<PeriodicalEditionViewModel, PeriodicalEdition>();

            });
        }
    }
}
