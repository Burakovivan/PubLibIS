using System.Collections.Generic;
using PubLibIS.ViewModels;

namespace PubLibIS.BLL.Interfaces
{
    public interface IPublishingHouseService
    {
        int CreatePublishinHouse(PublishingHouseViewModel ph);
        void PublishingHouse(int id);
        PublishingHouseViewModel GetPublishingHouseViewModel(int id);
        IEnumerable<PublishingHouseViewModel> GetPublishingHouseViewModelList();
        void UpdatePublishingHouse(PublishingHouseViewModel ph);
    }
}