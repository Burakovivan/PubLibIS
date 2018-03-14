using System.Collections.Generic;
using PubLibIS.ViewModels;

namespace PubLibIS.BLL.Interfaces
{
    public interface IPeriodicalService: IJsonProcessor
    {
        int CreatePeriodical(PeriodicalViewModel periodical);
        void CreatePeriodicalEdition(PeriodicalEditionViewModel edition);
        PeriodicalViewModel GetPeriodicalViewModel(int id);
        PeriodicalEditionViewModel GetPeriodicalEditionViewModel(int id);
        IEnumerable<PeriodicalViewModel> GetPeriodicalViewModelList();
        int GetNextEditionNumberByPeriodicalId(int periodicalId);
        IEnumerable<PeriodicalEditionViewModel> GetPeriodicalEditionViewModelListByPeriodicalId(int periodicalId);
        void UpdatePeriodical(PeriodicalViewModel periodical);
        void UpdatePeriodicalEdition(PeriodicalEditionViewModel periodical);
        void DeletePeriodical(int id);
        void DeletePeriodicalEdition(int id);
        IEnumerable<PeriodicalTypeViewModel> GetPeriodicalTypeViewModelList();
        PeriodicalCatalogViewModel GetPeriodicalCatalogViewModel(int skip, int take);
    }
}