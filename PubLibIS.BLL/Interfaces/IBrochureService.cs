using System.Collections.Generic;
using PubLibIS.ViewModels;

namespace PubLibIS.BLL.Interfaces
{
    public interface IBrochureService: IJsonProcessor
    {
        int CreateBrochure(BrochureViewModel brochure);
        void DeleteBrochure(int id);
        BrochureViewModel GetBrochureViewModel(int id);
        IEnumerable<BrochureViewModel> GetGetBrochureViewModelList();
        void UpdateBrochure(BrochureViewModel brochure);
    }
}