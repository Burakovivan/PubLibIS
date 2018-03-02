using PubLibIS.DAL.Model;
using System.Collections.Generic;

namespace PubLibIS.DAL.Interfaces
{
    public interface IBrochureRepository
    {
        int Create(Brochure book);
        Brochure Read(int bookId);
        IEnumerable<Brochure> Read();
        IEnumerable<Brochure> Read(int skip, int take);
        void Update(Brochure book);
        void Delete(int bookId);
    }
}
