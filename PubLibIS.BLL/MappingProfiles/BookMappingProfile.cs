using AutoMapper;
using PubLibIS.DAL.Models;
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
                    book => book.Authors,
                    opt => opt.MapFrom(bookVM => bookVM.Authors));

            CreateMap<Book, BookViewModel>()
                .ForMember(
                    bookVM => bookVM.ReleaseDate,
                        opt => opt.MapFrom(book => book.PublishedBooks.Any() ? book.PublishedBooks.Select(x => x.DateOfPublication).Min() : null))
                .ForMember(
                    bookVM => bookVM.Authors,
                    opt => opt.MapFrom(book => book.Authors.Select(x => x.Author)));

            CreateMap<PublishedBook, PublishedBookViewModel>()
                .ForMember(
                    pBook => pBook.Book,
                    opt => opt.MapFrom(pBookVM => pBookVM.Book));

            CreateMap<PublishedBookViewModel, PublishedBook>()
                .ForMember(
                    pBookVM => pBookVM.Book,
                    opt => opt.MapFrom(pBook => pBook.Book));

            CreateMap<AuthorViewModel, AuthorInBook>()
                .ForMember(
                    ainb => ainb.Author,
                    opt => opt.MapFrom(avm => avm));
        }
    }
}
