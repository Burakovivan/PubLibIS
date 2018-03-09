using PubLibIS.DAL.Models;
using System.Collections.Generic;

namespace PubLibIS.DAL.Interfaces
{
    public interface IBrochureRepository
    {
        int Create(Brochure book);
        Brochure Get(int bookId);
        IEnumerable<Brochure> Get();
        IEnumerable<Brochure> Get(int skip, int take);
        void Update(Brochure book);
        void Delete(int bookId);
    }
}
