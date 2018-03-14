using System.Collections.Generic;
using PubLibIS.ViewModels;

namespace PubLibIS.BLL.Interfaces
{
    public interface IBrochureService: IJsonProcessor
    {
        int CreateBrochure(BrochureViewModel brochure);
        void DeleteBrochure(int id);
        BrochureViewModel GetBrochureViewModel(int id);
        IEnumerable<BrochureViewModel> GetBrochureViewModelList();
        BrochureCatalogViewModel GetBrochureCatalogViewModel(int skip, int take);
        void UpdateBrochure(BrochureViewModel brochure);
    }
}