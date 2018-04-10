using PubLibIS.Domain.Entities;
using System.Collections.Generic;

namespace PubLibIS.DAL.Interfaces
{
    public interface IBrochureRepository
    {
        int Create(Brochure book);
        Brochure Get(int bookId);
        int Count();
        IEnumerable<Brochure> GetList();
        IEnumerable<Brochure> GetList(IEnumerable<int> idList);
        IEnumerable<Brochure> GetList(int skip, int take);
        void Update(Brochure book);
        void Delete(int bookId);
    }
}
