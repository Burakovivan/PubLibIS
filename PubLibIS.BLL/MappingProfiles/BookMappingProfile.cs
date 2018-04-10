using AutoMapper;
using PubLibIS.DAL.ResponseModels;
using PubLibIS.Domain.Entities;
using PubLibIS.ViewModels;
using System.Linq;

namespace PubLibIS.BLL.MappingProfiles
{
    public class BookMappingProfile : Profile
    {
        public BookMappingProfile()
        {
            CreateMap<BookViewModel, Book>()
            .ForMember(
            book => book.CreatedDate,
            opt => opt.Ignore())
            .ForMember(
            book => book.ModifiedDate,
            opt => opt.Ignore())
            .ForMember(
            book => book.CreatedDate,
            opt => opt.Ignore());

            CreateMap<BookViewModel, Book>()
           .ForMember(
           book => book.CreatedDate,
           opt => opt.Ignore())
           .ForMember(
           book => book.ModifiedDate,
           opt => opt.Ignore())
           .ForMember(
           book => book.CreatedDate,
           opt => opt.Ignore()).ReverseMap();

            CreateMap<BookViewModel, GetBookResponseModel>()
        .ForMember(
            book => book.Authors,
            opt => opt.MapFrom(bookVM => bookVM.Authors));

            CreateMap<GetBookResponseModel, BookViewModel>()
                .ForMember(
                    bookVM => bookVM.ReleaseDate,
                        opt => opt.MapFrom(book => book.PublishedBooks.Any() ? book.PublishedBooks.Select(x => x.DateOfPublication).Min() : null))
                .ForMember(
                    bookVM => bookVM.Authors,
                    opt => opt.MapFrom(book => book.Authors.Select(x => x.Author)))
                .ForMember(
                    bookVM => bookVM.Publications,
                    opt => opt.MapFrom(book => book.PublishedBooks));

            CreateMap<GetBookResponseModel, BookViewModelSlim>()
               .ForMember(
                   bookVM => bookVM.ReleaseDate,
                       opt => opt.MapFrom(book => book.PublishedBooks.Any() ? book.PublishedBooks.Select(x => x.DateOfPublication).Min() : null))
              .ForMember(
                   bookVM => bookVM.Authors,
                       opt => opt.MapFrom(book => string.Join(", ", book.Authors.Select(ainb => $"{ainb.Author.SecondName}" +
                    (string.IsNullOrEmpty(ainb.Author.FirstName) ? " " : $" {ainb.Author.FirstName.TrimStart()[0]}.") +
                    (string.IsNullOrEmpty(ainb.Author.Patronymic) ? " " : $" {ainb.Author.Patronymic.TrimStart()[0]}.")))))
               .ForMember(
                   bookVM => bookVM.CountOfPublication,
                   opt => opt.MapFrom(book => book.PublishedBooks.Count()));


            CreateMap<PublishedBook, PublishedBookViewModel>()
                .ForMember(
                    pBook => pBook.Book,
                    opt => opt.MapFrom(pBookVM => pBookVM.Book));

            CreateMap<PublishedBookViewModel, PublishedBook>()
                .ForMember(
                    pBookVM => pBookVM.Book,
                    opt => opt.MapFrom(pBook => pBook.Book))
                      .ForMember(
                    pBookVM => pBookVM.Book_Id,
                    opt => opt.MapFrom(pBook => pBook.Book_Id));

            CreateMap<AuthorViewModel, AuthorInBook>()
            .ForMember(
                    ainb => ainb.Author_Id,
                    opt => opt.MapFrom(avm => avm.Id))
                .ForMember(
                    ainb => ainb.Author,
                    opt => opt.MapFrom(avm => avm));
        }
    }
}
