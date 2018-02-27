using AutoMapper;
using PubLibIS_DAL.Model;
using System.Collections.Generic;
using ViewModels;

namespace PubLibIS_BLL.Mappers
{
    public static class PublishedBookMapper
    {
        //SLIM
        public static PublishedBookViewModelSlim MapOneUpSLim(PublishedBook book)
        {
            return Mapper.Map<PublishedBook, PublishedBookViewModelSlim>(book);
        }

        public static IEnumerable<PublishedBookViewModelSlim> MapManyUpSlim(IEnumerable<PublishedBook> books)
        {
            return Mapper.Map<IEnumerable<PublishedBook>, IEnumerable<PublishedBookViewModelSlim>>(books);
        }

        public static PublishedBook MapOneDownSlim(PublishedBookViewModelSlim book)
        {
            return Mapper.Map<PublishedBookViewModelSlim, PublishedBook>(book);
        }

        public static IEnumerable<PublishedBook> MapManyUpSlim(IEnumerable<PublishedBookViewModelSlim> books)
        {
            return Mapper.Map<IEnumerable<PublishedBookViewModelSlim>, IEnumerable<PublishedBook>>(books);
        }


        //FULL
        public static PublishedBookViewModel MapOneUp(PublishedBook book)
        {
            return Mapper.Map<PublishedBook, PublishedBookViewModel>(book);
        }

        public static IEnumerable<PublishedBookViewModel> MapManyUp(IEnumerable<PublishedBook> books)
        {
            return Mapper.Map<IEnumerable<PublishedBook>, IEnumerable<PublishedBookViewModel>>(books);
        }

        public static PublishedBook MapOneDown(PublishedBookViewModel book)
        {
            return Mapper.Map<PublishedBookViewModel, PublishedBook>(book);
        }

        public static IEnumerable<PublishedBook> MapManyUp(IEnumerable<PublishedBookViewModel> books)
        {
            return Mapper.Map<IEnumerable<PublishedBookViewModel>, IEnumerable<PublishedBook>>(books);
        }

    }
}
