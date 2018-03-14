using System.Collections.Generic;
using PubLibIS.DAL.Models;
using PubLibIS.ViewModels;

namespace PubLibIS.BLL.Interfaces
{
    public interface IPublishingHouseService: IJsonProcessor
    {
        int CreatePublishinHouse(PublishingHouseViewModel ph);
        void DeletePublishingHouse(int id);
        PublishingHouseViewModel GetPublishingHouseViewModel(int id);
        IEnumerable<PublishingHouseViewModel> GetPublishingHouseViewModelList();
        void UpdatePublishingHouse(PublishingHouseViewModel ph);
        IEnumerable<PublishingHouseViewModelSlim> GetPublishingHouseViewModelSlimList();
    }
}