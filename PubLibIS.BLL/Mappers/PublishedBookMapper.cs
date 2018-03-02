using AutoMapper;
using PubLibIS.DAL.Model;
using System.Collections.Generic;
using PubLibIS.ViewModels;

namespace PubLibIS.BLL.Mappers
{
    public static class PublishedBookMapper
    {
        //SLIM
        public static PublishedBookViewModelslim MapOneUpSLim(PublishedBook book)
        {
            return Mapper.Map<PublishedBook, PublishedBookViewModelslim>(book);
        }

        public static IEnumerable<PublishedBookViewModelslim> MapManyUpSlim(IEnumerable<PublishedBook> books)
        {
            return Mapper.Map<IEnumerable<PublishedBook>, IEnumerable<PublishedBookViewModelslim>>(books);
        }

        public static PublishedBook MapOneDownSlim(PublishedBookViewModelslim book)
        {
            return Mapper.Map<PublishedBookViewModelslim, PublishedBook>(book);
        }

        public static IEnumerable<PublishedBook> MapManyUpSlim(IEnumerable<PublishedBookViewModelslim> books)
        {
            return Mapper.Map<IEnumerable<PublishedBookViewModelslim>, IEnumerable<PublishedBook>>(books);
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
