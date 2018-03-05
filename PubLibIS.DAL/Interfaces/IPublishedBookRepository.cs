using PubLibIS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubLibIS.DAL.Interfaces
{
    public interface IPublishedBookRepository
    {
        int Create(PublishedBook pBook);
        PublishedBook Read(int pBookId);
        IEnumerable<PublishedBook> Read();
        IEnumerable<PublishedBook> ReadByBookId(int bookId);
        IEnumerable<PublishedBook> Read(int skip, int take);
        void Update(PublishedBook pBook);
        void Delete(int pBookId);
    }
}
