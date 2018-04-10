using PubLibIS.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using PubLibIS.Domain.Entities;
using PubLibIS.DAL.ResponseModels;

namespace PubLibIS.DAL.Repositories.Dapper
{
    public class PublishedBookRepository : Repository<PublishedBook>, IPublishedBookRepository
    {

        public PublishedBookRepository(DapperConnectionFactory dapperConnectionFactory)
        : base(dapperConnectionFactory) { }

        public IEnumerable<PublishedBook> GetPublishedBookByBookId(int bookId)
        {
            var bookRepository = new BookRepository(dapperConnectionFactory);
            var publishingHouseRepository = new PublishingHouseRepository(dapperConnectionFactory);
            IEnumerable<PublishedBook> publishedBookList = GetList().Where(pb => pb.Book_Id == bookId);
            publishedBookList.ToList().ForEach(pb =>
            {
                pb.PublishingHouse = pb.PublishingHouse_Id.HasValue ? publishingHouseRepository.Get(pb.PublishingHouse_Id.Value) : null;
                pb.Book = pb.Book_Id.HasValue ? bookRepository.Get(pb.Book_Id.Value) : null;
            });
            return publishedBookList;
        }


    }
}
