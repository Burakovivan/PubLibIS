using PubLibIS.DAL.ResponseModels;
using PubLibIS.Domain.Entities;
using System.Collections.Generic;

namespace PubLibIS.DAL.Interfaces
{
    public interface IPublishedBookRepository
    {
        int Create(PublishedBook pBook);
        PublishedBook Get(int pBookId);
        IEnumerable<PublishedBook> GetList();
        IEnumerable<PublishedBook> GetPublishedBookByBookId(int bookId);
        IEnumerable<PublishedBook> GetList(int skip, int take);
        void Update(PublishedBook pBook);
        void Delete(int pBookId);
    }
}
