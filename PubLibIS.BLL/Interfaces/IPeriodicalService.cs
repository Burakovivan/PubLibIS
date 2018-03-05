using System.Collections.Generic;
using PubLibIS.ViewModels;

namespace PubLibIS.BLL.Interfaces
{
    public interface IPeriodicalService
    {
        int CreatePeriodical(PeriodicalViewModel periodical);
        void CreatePeriodicalEdition(PeriodicalEditionViewModel edition);
        PeriodicalViewModel GetPeriodicalViewModel(int id);
        IEnumerable<PeriodicalViewModel> GetPeriodicalViewModelList();
        int GetNextEditionNumberByPeriodicalId(int periodicalId);
        IEnumerable<PeriodicalEditionViewModel> GetPeriodicalEditionViewModelByPeriodicalId(int periodicalId);
        void UpdatePeriodical(PeriodicalViewModel periodical);
        void UpdatePeriodicalEdition(PeriodicalEditionViewModel periodical);
        void DeletePeriodical(int id);
        void DeletePeriodicalEdition(int id);
        IEnumerable<PeriodicalTypeViewModel> GetPeriodicalTypeViewModelList();
    }
}